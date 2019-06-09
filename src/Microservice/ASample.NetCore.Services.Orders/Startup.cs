using System;
using System.Reflection;
using ASample.NetCore.Dispatchers;
using ASample.NetCore.MongoDb;
using ASample.NetCore.RabbitMq;
using ASample.NetCore.Services.Orders.Messages.Commands;
using ASample.NetCore.Services.Orders.Messages.Events.Rejected;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASample.NetCore.Orders
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                   .AsImplementedInterfaces();

            builder.Populate(services);
            builder.AddMongo();
            builder.AddRabbitMq();
            builder.AddDispatchers();
            //builder.AddRedis();
            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseRabbitMq()
               .SubscribeCommand<CreateOrderCommand>(onError: (c, e) =>
                   new CreateOrderRejectedEvent(c.Id, c.CustomerId, e.Message, e.Code))
               .SubscribeCommand<ApproveOrderCommand>(onError: (c, e) =>
                   new ApproveOrderRejectedEvent(c.Id, e.Message, e.Code));
               //.SubscribeCommand<CancelOrder>(onError: (c, e) =>
               //    new CancelOrderRejected(c.Id, c.CustomerId, e.Message, e.Code))
               //.SubscribeCommand<RevokeOrder>(onError: (c, e) =>
               //    new RevokeOrderRejected(c.Id, c.CustomerId, e.Message, e.Code))
               //.SubscribeCommand<CompleteOrder>(onError: (c, e) =>
               //    new CompleteOrderRejected(c.Id, c.CustomerId, e.Message, e.Code))
               //.SubscribeCommand<CreateOrderDiscount>()
               //.SubscribeEvent<CustomerCreated>(@namespace: "customers");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
