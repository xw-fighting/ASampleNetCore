using System;
using System.Reflection;
using ASample.NetCore.Dispatchers;
using ASample.NetCore.Jaeger;
using ASample.NetCore.NonRelationalDb;
using ASample.NetCore.RabbitMq;
using ASample.NetCore.Redis;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASample.NetCore.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer();
            services.AddHttpClient();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddJaeger();
            services.AddSignalR();
            //services.AddRedis();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                    .AsImplementedInterfaces();

            builder.Populate(services);
            builder.AddDispatchers();
            builder.AddRabbitMq();
            builder.AddCustomerRedis();
           
            //builder.AddMongoRepository<Product>("Products");
            //builder.AddMongoRepository<Order>("Orders");
            //builder.RegisterServiceForwarder<ICustomersService>("customers-service");
            //builder.RegisterServiceForwarder<IProductsService>("products-service");
            //builder.RegisterServiceForwarder<IOrdersService>("orders-service");

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
            app.UseCors("CorsPolicy");
            //app.UseAllForwardedHeaders();
            //app.UseAuthentication();
            //app.UseAccessTokenValidator();
            //app.UseRabbitMq()
            //    .SubscribeCommand<UserInfoCreateCommand>(onError: (cmd, ex)
            //        => new CreateUserInfoRejected(cmd.Id, ex.Message, "customer_not_found"))
            //    .SubscribeEvent<UserInfoCreateEvent>(@namespace: "userinfo");
            //.SubscribeEvent<OrderCompleted>(@namespace: "orders");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
