using System;
using System.Reflection;
using ASample.NetCore.RelationalDb;
using ASample.NetCore.Services.IdentityServers.Repositories;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using IdentityServer4.EntityFramework.Stores;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ASample.NetCore.Services.IdentityServers
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //sqlserver 数据库
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = Configuration.GetSection("sqlserver:ConnectionString").Value;
            services.Configure<DbOptions>(Configuration.GetSection("sqlserver"));
            services.AddRelationalDb<ASampleSqlServerDbContext>();
            services.AddIdentityServer()
                .AddTestUsers(TestUsers.Users)
                .AddDeveloperSigningCredential()
                // Add customer profile service
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                ;

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            //services.AddAuthentication()
            //    .AddGoogle("Google", options =>
            //    {
            //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            //        options.ClientId = "658795020276-tbu1th034kvcq34ro21qosgouqh6rijs.apps.googleusercontent.com";
            //        options.ClientSecret = "Tc-gfiVKl2XyfdPIlgLd3l0P";
            //    });
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IClientStore, ClientStore>();
            services.AddTransient<IResourcesRepository, ResourcesRepository>();

            services.AddAuthorization();
            services.AddControllersWithViews();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsImplementedInterfaces();
            //builder.AddMongo<ASampleMongoDbContext>();

            
            builder.Populate(services);
            //builder.AddDispatchers();
            //builder.AddRabbitMq();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //InitializeDatabase(app);
            app.UseStaticFiles();
            app.UseRouting();
            //app.UseCors("default");

            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
