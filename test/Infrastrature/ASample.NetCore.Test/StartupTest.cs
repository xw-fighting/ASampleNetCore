using System;
using System.Reflection;
using ASample.NetCore.Dispatchers;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASample.NetCore.Test
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
            services.Configure<RedisCacheOptions>(configuration.GetSection(dbType)); //注入配置数据
            
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

            Container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(Container);
            return serviceProvider;
        }
    }
}
