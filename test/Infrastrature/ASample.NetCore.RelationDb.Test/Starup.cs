using ASample.NetCore.Dispatchers;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace ASample.NetCore.RelationDb.Test
{
    public class Startup
    {
        public static IContainer Container { get; private set; }

        public static IServiceProvider InitStartup()
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();
            var configuration = config.Build();
            //services.AddSingleton<IConfiguration>(Configuration);
            var builder = new ContainerBuilder();
            builder.RegisterInstance<IConfiguration>(configuration);
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            //builder.Populate(services);
            builder.AddDispatchers();
            //builder.AddRabbitMq();
            //builder.AddMongo();
          
            Container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(Container);
            return serviceProvider;
        }
    }
}
