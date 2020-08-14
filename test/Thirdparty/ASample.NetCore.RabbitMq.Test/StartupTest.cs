using ASample.NetCore.RabbitMq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using ASample.NetCore.RabbitMq.Publish;
using Jaeger;
using Microsoft.Extensions.Options;

namespace ASample.NetCore.RabbitMq.Test
{
    public class StartupTest
    {
        public static Autofac.IContainer Container { get; private set; }

        public static IServiceProvider InitStartup(string rabbitMq)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();
            
                
            //services.Configure<DbOptions>(Configuration.GetSection("sql"));
            
            var configuration = config.Build();
            var services = new ServiceCollection();
            //services.AddOptions<RabbitMqOption>();  //ע��IOptions<T>�������Ϳ�����DI�����л�ȡIOptions<T>��

            //services.Configure<RabbitMqOption>(configuration.GetSection(rabbitMq)); //ע����������
            //services.Configure<RabbitMqOption>(configuration.GetSection(rabbitMq));
            services.AddSingleton<IConfiguration>(configuration);
            //services.AddSingleton<IUnitOfWork<ASampleSqlServerDbContext>, UnitOfWork<ASampleSqlServerDbContext>>();
            services.BuildServiceProvider();

            var builder = new ContainerBuilder();
            builder.RegisterInstance<IConfiguration>(configuration);
            ////builder.RegisterInstance<IOptions<RabbitMqOption>>();
            //builder.RegisterType<RabbitMqOption>()
            //    .As<IOptions<RabbitMqOption>>()
            //    .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            //builder.Populate(services);
            //builder.AddDispatchers();
           
            builder.AddASampleRabbitMq();

            Container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(Container);
            return serviceProvider;
        }
    }
}
