using ASample.NetCore.Dispatchers;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.RelationalDb;
using ASample.NetCore.RelationalDb.Test.Model;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;

namespace ASample.NetCore.RelationDb.Test
{
    public class StartupTest
    {
        public static IContainer Container { get; private set; }

        public static IServiceProvider InitStartup(string dbType)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();
            
                
            //services.Configure<DbOptions>(Configuration.GetSection("sql"));
            
            var configuration = config.Build();
            var services = new ServiceCollection();
            services.AddOptions();  //注入IOptions<T>，这样就可以在DI容器中获取IOptions<T>了
            services.Configure<DbOptions>(configuration.GetSection(dbType)); //注入配置数据
            if(dbType == "sql")
            {
                services.AddRelationalDb<ASampleSqlServerDbContext>();
            }
            if(dbType == "postgre")
            {
                services.AddRelationalDb<ASamplePostgreDbContext>();
            }

            if (dbType == "mysql")
            {
                services.AddRelationalDb<ASampleMySqlDbContext>();
            }
            services.AddSingleton<IConfiguration>(configuration);
            //services.AddSingleton<IUnitOfWork<ASampleSqlServerDbContext>, UnitOfWork<ASampleSqlServerDbContext>>();
            services.BuildServiceProvider();
            var builder = new ContainerBuilder();
            builder.RegisterInstance<IConfiguration>(configuration);
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            //builder.Populate(services);
            builder.AddDispatchers();
            //builder.AddRabbitMq();
            //builder.AddMongo();
            //builder.RegisterType<>().As<IOptions<DbOptions>>();
           
            
            if (dbType == "sql")
            {
                builder.Register(context =>
                {
                    var provider = services.BuildServiceProvider();
                    var options1 = provider.GetServices<IOptions<DbOptions>>();
                    var dbContext = new ASampleSqlServerDbContext(options1.FirstOrDefault());
                    return dbContext;
                }).SingleInstance();
                builder.RegisterType<UnitOfWork<ASampleSqlServerDbContext>>().As<IUnitOfWork<ASampleSqlServerDbContext>>();
                builder.RegisterType<Repository<ASampleSqlServerDbContext, User>>().As<IRepository<User>>();
            }

            if (dbType == "postgre")
            {
                builder.Register(context =>
                {
                    var provider = services.BuildServiceProvider();
                    var options1 = provider.GetServices<IOptions<DbOptions>>();
                    var dbContext = new ASamplePostgreDbContext(options1.FirstOrDefault());
                    return dbContext;
                }).SingleInstance();
                builder.RegisterType<UnitOfWork<ASamplePostgreDbContext>>().As<IUnitOfWork<ASamplePostgreDbContext>>();
                builder.RegisterType<Repository<ASamplePostgreDbContext, User>>().As<IRepository<User>>();
            }

            if (dbType == "mysql")
            {
                builder.Register(context =>
                {
                    var provider = services.BuildServiceProvider();
                    var options1 = provider.GetServices<IOptions<DbOptions>>();
                    var dbContext = new ASampleMySqlDbContext(options1.FirstOrDefault());
                    return dbContext;
                }).SingleInstance();
                builder.RegisterType<UnitOfWork<ASampleMySqlDbContext>>().As<IUnitOfWork<ASampleMySqlDbContext>>();
                builder.RegisterType<Repository<ASampleMySqlDbContext, User>>().As<IRepository<User>>();
            }

            Container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(Container);
            return serviceProvider;
        }
    }
}
