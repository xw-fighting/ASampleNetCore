using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ASample.NetCore.Consul;
using ASample.NetCore.Dispatchers;
using ASample.NetCore.Jaeger;
using ASample.NetCore.MongoDb;
using ASample.NetCore.Mvc;
using ASample.NetCore.RabbitMq;
using ASample.NetCore.Redis;
using ASample.NetCore.Services.Products.Domain;
using ASample.NetCore.Services.Products.Messages.Commands;
using ASample.NetCore.Services.Products.Messages.Events.Rejected;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASample.NetCore.Services.Products
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            services.AddConsul();
            services.AddJaeger();
            services.AddOpenTracing();
            services.AddRedis();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                   .AsImplementedInterfaces();

            builder.Populate(services);
            builder.AddMongo();
            builder.AddMongoRepository<Product>("Products");
            builder.AddRabbitMq();
            builder.AddDispatchers();
            //builder.AddCustomerRedis();
            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, IApplicationLifetime applicationLifetime, IConsulClient client)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseAllForwardedHeaders();



            //app.UseHttpsRedirection();
            app.UseAllForwardedHeaders();
            app.UseErrorHandler();
            app.UseServiceId();
            app.UseMvc();
            app.UseRabbitMq()
                .SubscribeCommand<CreateProductCommand>(onError: (c, e) =>
                    new CreateProductRejectedEvent(c.Id, e.Message, e.Code))
                .SubscribeCommand<UpdateProductCommand>(onError: (c, e) =>
                    new UpdateProductRejectedEvent(c.Id, e.Message, e.Code))
                .SubscribeCommand<DeleteProductCommand>(onError: (c, e) =>
                    new DeleteProductRejectedEvent(c.Id, e.Message, e.Code))
                .SubscribeCommand<ReserveProductCommand>(onError: (c, e) =>
                    new ReserveProductsRejectedEvent(c.OrderId, e.Message, e.Code))
                .SubscribeCommand<ReleaseProductCommand>(onError: (c, e) =>
                    new ReleaseProductsRejectedEvent(c.OrderId, e.Message, e.Code));
            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);
                Container.Dispose();
            });
        }
    }
}
