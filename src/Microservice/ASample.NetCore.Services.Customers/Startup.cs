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
using ASample.NetCore.Services.Customers.Domain;
using ASample.NetCore.Services.Customers.Messages.Commands;
using ASample.NetCore.Services.Customers.Messages.Events;
using ASample.NetCore.Services.Customers.Messages.Events.Orders;
using ASample.NetCore.Services.Customers.Messages.Events.Products;
using ASample.NetCore.Services.Customers.Messages.Events.Rejected;
using ASample.NetCore.Services.Customers.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASample.NetCore.Services.Customers
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
            //services.AddRedis();
            services.AddInitializers(typeof(IMongoDbInitializer));
            services.RegisterServiceForwarder<IProductsService>("products-service");

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddDispatchers();
            builder.AddRabbitMq();
            builder.AddCustomerRedis();
            builder.AddMongo();
            builder.AddMongoRepository<Cart>("Carts");
            builder.AddMongoRepository<Customer>("Customers");
            builder.AddMongoRepository<Product>("Products");

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
            app.UseAllForwardedHeaders();
            //app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseServiceId();
            app.UseMvc();
            app.UseRabbitMq()
                .SubscribeCommand<CreateCustomerCommand>(onError: (c, e) =>
                    new CreateCustomerRejectEvent(c.Id, e.Message, e.Code))
                .SubscribeCommand<AddProductToCartCommand>(onError: (c, e) =>
                    new AddProductToCartRejectedEvent(c.CustomerId, c.ProductId, c.Quantity, e.Message, e.Code))
                .SubscribeCommand<DeleteProductFromCartCommand>(onError: (c, e) =>
                    new DeleteProductFromCartRejectedEvent(c.CustomerId, c.ProductId, e.Message, e.Code))
                .SubscribeCommand<ClearCartCommand>(onError: (c, e) =>
                    new ClearCartRejectedEvent(c.CustomerId, e.Message, e.Code))
                .SubscribeEvent<SignedUpEvent>(@namespace: "identity")
                .SubscribeEvent<ProductCreatedEvent>(@namespace: "products")
                .SubscribeEvent<ProductUpdatedEvent>(@namespace: "products")
                .SubscribeEvent<ProductDeletedEvent>(@namespace: "products")
                .SubscribeEvent<OrderApprovedEvent>(@namespace: "orders")
                .SubscribeEvent<OrderCompletedEvent>(@namespace: "orders")
                .SubscribeEvent<OrderCancelEvent>(@namespace: "orders");

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
