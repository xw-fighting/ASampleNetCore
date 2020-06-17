using System;
using System.Reflection;
using ASample.NetCore.Consul;
using ASample.NetCore.Dispatchers;
using ASample.NetCore.Jaggers;
using ASample.NetCore.MongoDb;
using ASample.NetCore.Mvc;
using ASample.NetCore.RabbitMq;
using ASample.NetCore.Redis;
using ASample.NetCore.RestEase;
using ASample.NetCore.Services.Orders.Domain;
using ASample.NetCore.Services.Orders.Messages.Commands;
using ASample.NetCore.Services.Orders.Messages.Events;
using ASample.NetCore.Services.Orders.Messages.Events.Rejected;
using ASample.NetCore.Services.Orders.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Consul;
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
            services.AddCustomMvc();
            //services.AddSwaggerDocs();
            services.AddConsul();
            services.AddJaeger();
            services.AddOpenTracing();
            //services.AddInitializers(typeof(IMongoDbInitializer));
            services.RegisterServiceForwarder<IProductsService>("products-service");
            services.RegisterServiceForwarder<ICustomersService>("customers-service");

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                   .AsImplementedInterfaces();

            builder.Populate(services);
            builder.AddMongo();
            builder.AddMongoRepository<Order>("Orders");
            builder.AddMongoRepository<OrderItem>("OrderItems");
            builder.AddMongoRepository<Customer>("Customers");

            builder.AddRabbitMq();
            builder.AddDispatchers();
            builder.AddCustomerRedis();
            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime, IConsulClient client)
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
                   new ApproveOrderRejectedEvent(c.Id, e.Message, e.Code))
               .SubscribeCommand<CancelOrderCommand>(onError: (c, e) =>
                   new CancelOrderRejectedEvent(c.Id, c.CustomerId, e.Message, e.Code))
               .SubscribeCommand<RevokeOrderCommand>(onError: (c, e) =>
                   new RevokeOrderRejectedEvent(c.Id, c.CustomerId, e.Message, e.Code))
               .SubscribeCommand<CompleteOrderCommand>(onError: (c, e) =>
                   new CompleteOrderRejectedEvent(c.Id, c.CustomerId, e.Message, e.Code))
               .SubscribeCommand<CreateOrderDiscountCommand>()
               .SubscribeEvent<CustomerCreatedEvent>(@namespace: "customers");
            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);
                Container.Dispose();
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
