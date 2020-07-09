using ASample.NetCore.RabbitMq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using ASample.NetCore.RabbitMq.Publish;

namespace ASample.NetCore.RabbitMq.Test
{
    public class StartupTest
    {
        public static Autofac.IContainer Container { get; private set; }

        public static IServiceProvider InitStartup(string dbType)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();
            
                
            //services.Configure<DbOptions>(Configuration.GetSection("sql"));
            
            var configuration = config.Build();
            var services = new ServiceCollection();
            services.AddOptions();  //ע��IOptions<T>�������Ϳ�����DI�����л�ȡIOptions<T>��
            services.Configure<RabbitMqOption>(configuration.GetSection(dbType)); //ע����������
            
            services.AddSingleton<IConfiguration>(configuration);
            //services.AddSingleton<IUnitOfWork<ASampleSqlServerDbContext>, UnitOfWork<ASampleSqlServerDbContext>>();
            services.BuildServiceProvider();


            var builder = new ContainerBuilder();
            builder.RegisterInstance<IConfiguration>(configuration);
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            //builder.Populate(services);
            //builder.AddDispatchers();
            builder.AddASampleRabbitMq();
            builder.RegisterType<ASampleRabbitMqClient>()
                .As<IASampleRabbitMqClient>()
                .InstancePerLifetimeScope();

            Container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(Container);
            return serviceProvider;
        }
    }
}
