﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.SqlServer.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ASample.NetCore.SqlServer;
using Autofac;
using ASample.NetCore.Dispatchers;
using Autofac.Extensions.DependencyInjection;
using ASample.NetCore.SqlServerWebSite.Domain;

namespace ASample.NetCore.SqlServerWebSite
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
            services.Configure<SqlServerOptions>(Configuration.GetSection("sql"));
            services.AddEntityFrameworkSqlServer()
               .AddSqlServer<ASampleDbContext>();
            //services.AddSqlServer<ASampleDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddDispatchers();
            //builder.AddMongo();
            //builder.AddMongoRepository<Customer>("Customers");
            //builder.AddMongoRepository<Discount>("Discounts");
            builder.AddSqlServerRepository<ASampleDbContext, User>();
            builder.AddSqlServerRepository<ASampleDbContext, UserInfo>();
            //builder.AddRabbitMq();

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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}