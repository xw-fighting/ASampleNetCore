using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.SqlServerDb.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ASample.NetCore.SqlServerDb;
using Autofac;
using ASample.NetCore.Dispatchers;
using Autofac.Extensions.DependencyInjection;
using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.MySqlDb.Options;
using ASample.NetCore.MySqlDb;
using ASample.NetCore.PostgreDb.Options;
using ASample.NetCore.PostgreDb;
using Swashbuckle.AspNetCore.Swagger;
using ASample.NetCore.MongoDb;

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
            services.Configure<SqlServerOptions>(Configuration.GetSection("sql"));
            services.AddEntityFrameworkSqlServer()
               .AddSqlServer<ASampleSqlServerDbContext>();

            services.Configure<PostgreOptions>(Configuration.GetSection("postgre"));
            services.AddPostgre<AsamplePostgreDbContext>();

            services.Configure<MySqlOptions>(Configuration.GetSection("mysql"));
            services.AddMySql<ASampleMySqlDbContext>();

            services.Configure<MySqlOptions>(Configuration.GetSection("nongo"));
            services.AddMySql<ASampleMySqlDbContext>();


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

            builder.AddSqlServerRepository<ASampleSqlServerDbContext, User>();
            builder.AddSqlServerRepository<ASampleSqlServerDbContext, UserInfo>();

            builder.AddMySqlRepository<ASampleMySqlDbContext, User>();
            builder.AddMySqlRepository<ASampleMySqlDbContext, UserInfo>();

            builder.AddPostgreRepository<AsamplePostgreDbContext, User>();
            builder.AddPostgreRepository<AsamplePostgreDbContext, UserInfo>();
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

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AsamplePostgreDbContext>();
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
