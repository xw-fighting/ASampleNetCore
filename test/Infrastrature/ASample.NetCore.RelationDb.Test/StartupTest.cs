using ASample.NetCore.Dispatchers;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.RelationalDb;
using ASample.NetCore.RelationalDb.Options;
using ASample.NetCore.RelationalDb.Repositories;
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

        public static IServiceProvider InitStartup()
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();
            
                
            //services.Configure<DbOptions>(Configuration.GetSection("sql"));
            
            var configuration = config.Build();
            var services = new ServiceCollection();
            services.AddOptions();  //注入IOptions<T>，这样就可以在DI容器中获取IOptions<T>了
            services.Configure<DbOptions>(configuration.GetSection("sql")); //注入配置数据
            services.AddRelationalDb<ASampleSqlServerDbContext>();
            //services.AddSingleton<IConfiguration>(configuration);
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
            builder.Register(context =>
            {
                var provider = services.BuildServiceProvider();
                var options1 = provider.GetServices<IOptions<DbOptions>>();
                //创建带参数的实例
                var mongoClient = new ASampleSqlServerDbContext(options1.FirstOrDefault());
                //mongoClient.SetProperty(options);
                return mongoClient;
            }).SingleInstance();

            builder.RegisterType<UnitOfWork<ASampleSqlServerDbContext>>().As<IUnitOfWork<ASampleSqlServerDbContext>>();
            builder.RegisterType<Repository<ASampleSqlServerDbContext,User>>().As<IRepository<User>>();
            Container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(Container);
            return serviceProvider;
        }
    }
}
