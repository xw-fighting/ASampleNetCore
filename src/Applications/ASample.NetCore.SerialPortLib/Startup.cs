using System;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System.Reflection;
using ASample.NetCore.RabbitMq;
using ASample.NetCore.RabbitMq.Publish;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace ASample.NetCore.SerialPortLib
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public static IContainer Container { get; private set; }


        public void ConfigureServices(IServiceCollection services)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables().Build();

            //services.AddHangfire(configuration =>
            //{
            //    //使用sql 数据库保存一些定时作业记录
            //    configuration.UseSqlServerStorage(Configuration.GetSection("sql:connectionString").Value);
            //});
            //services.AddARabbitMqClient();

            #region mq
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.RegisterInstance<IConfiguration>(Configuration);
            builder.AddASampleRabbitMq();

            Container = builder.Build();

            var serviceProvider = new AutofacServiceProvider(Container);
            // This will succeed.
            var asampleClient = serviceProvider.GetService<IASampleRabbitMqClient>();
            asampleClient.Subscribe(result =>
            {
                //send  result to SerialPort
                var portServices = new SendSerialPortServices();
                portServices.Wirte(result);
            });
            #endregion
        }

        public void Configure(IApplicationBuilder app)
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            //Configuration = builder.Build();
            ////客户端访问地址
            //app.UseHangfireDashboard("/hangfire");
            //app.UseHangfireServer();

            //builder.


        }
    }
}
