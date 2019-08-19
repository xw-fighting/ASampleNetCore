using ASample.NetCore.MongoDb.Test.Model;
using ASample.NetCore.NonInertialDb;
using ASample.NetCore.NonInertialDb.Options;
using ASample.NetCore.NonInertialDb.Repositories;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ASample.NetCore.MongoDb.Test
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
            
            services.AddSingleton<IConfiguration>(configuration);
            //services.AddSingleton<IUnitOfWork<ASampleSqlServerDbContext>, UnitOfWork<ASampleSqlServerDbContext>>();
            services.BuildServiceProvider();
            var builder = new ContainerBuilder();
            builder.RegisterInstance<IConfiguration>(configuration);
            //builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
            //    .AsImplementedInterfaces();
            //builder.Populate(services);
            //builder.AddDispatchers();
            //builder.AddRabbitMq();
            //builder.AddMongo();
            //builder.RegisterType<>().As<IOptions<DbOptions>>();
            builder.AddMongo<ASampleMongoDbContext>();
            //builder.RegisterType<UnitOfWork<ASampleMongoDbContext>>().As<IUnitOfWork<ASampleMongoDbContext>>();
            builder.RegisterType<Repository<ASampleMongoDbContext, User>>().As<IRepository<User>>();

            Container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(Container);
            return serviceProvider;
        }
    }
}
