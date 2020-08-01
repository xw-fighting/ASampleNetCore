using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using ASample.NetCore.Extension;
using ASample.NetCore.RabbitMq.Publish;
using RabbitMQ.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ASample.NetCore.RabbitMq
{
    public static class Extension
    {
        public static void AddASampleRabbitMq(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<RabbitMqOption>("rabbitmq");

                return options;
            }).SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<RabbitMqOption>();

                var rabbitFactory = new ConnectionFactory() 
                    { 
                        HostName = options.HostName,
                        Port = options.Port,
                        VirtualHost = options.VirtualHost,
                        UserName = options.UserName,
                        Password = options.Password
                    };

                var connect = rabbitFactory.CreateConnection();
                return connect;

            }).SingleInstance();

            builder.RegisterType<ASampleRabbitMqClient>()
                .As<IASampleRabbitMqClient>()
                .InstancePerLifetimeScope();

        }

        public static IServiceCollection AddARabbitMqClient(this IServiceCollection services)
        {
            services.AddSingleton<IASampleRabbitMqClient, ASampleRabbitMqClient>();
            return services;
        }
    }
}
