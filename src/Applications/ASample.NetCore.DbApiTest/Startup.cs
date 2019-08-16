using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using ASample.NetCore.Dispatchers;
using Autofac.Extensions.DependencyInjection;
using ASample.NetCore.DbApiTest.Domain;
using Swashbuckle.AspNetCore.Swagger;
using ASample.NetCore.MongoDb;
using ASample.NetCore.RelationalDb;
using ASample.NetCore.RelationalDb.Options;

namespace ASample.NetCore.DbApiTest
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
            //services.Configure<DbOptions>(Configuration.GetSection("sql"));
            //services.AddRelationalDb<ASampleSqlServerDbContext>(Configuration.GetSection("sql"));

            //services.Configure<DbOptions>(Configuration.GetSection("postgre"));
            //services.AddRelationalDb<AsamplePostgreDbContext>(Configuration.GetSection("postgre"));

            services.Configure<DbOptions>(Configuration.GetSection("mysql"));
            services.AddRelationalDb<ASampleMySqlDbContext>(Configuration.GetSection("mysql"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ASample API", Version = "v1" });
            });


            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddDispatchers();

            builder.AddMongo();
            builder.AddMongoRepository<User>("User");
           
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

            //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetRequiredService<AsamplePostgreDbContext>();
            //    context.Database.EnsureCreated();
            //}
            //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetRequiredService<ASampleSqlServerDbContext>();
            //    context.Database.EnsureCreated();
            //}
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ASampleMySqlDbContext>();
                context.Database.EnsureCreated();
            }
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASample API V1");
                //c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseMvc();

        }
    }
}
