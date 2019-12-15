using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ASample.NetCore
{
    public class AutofacServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
    {
        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            // read service collection to Autofac
            containerBuilder.Populate(services);

            return containerBuilder;
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            var applicationContainer = containerBuilder.Build();

            // creating the IServiceProvider out of the Autofac container
            return new AutofacServiceProvider(applicationContainer);
        }
    }
}
