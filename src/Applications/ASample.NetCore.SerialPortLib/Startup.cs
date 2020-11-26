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
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Log = Jaeger.Thrift.Log;

namespace ASample.NetCore.SerialPortLib
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public ILogger Logger { get; set; }

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

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddARabbitMqClient();

            //发送PEC进入打印机，服务重启重新发送PEC
            //var dic = Directory.GetCurrentDirectory();
            //var filePath = Path.Combine(dic, "BPP_PEC.pec");
            //var text = File.ReadAllText(filePath);
            //var portServices1 = new SendSerialPortServices();
            //portServices1.Wirte(text);

            //使用autofac 容器
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.RegisterInstance<IConfiguration>(Configuration);
            builder.AddASampleRabbitMq();
            Container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(Container);
            // This will succeed.
            var asampleClient = serviceProvider.GetService<IASampleRabbitMqClient>();

            //使用 net core 自己的容器

            //var sc = new ServiceCollection();
            //new Startup().ConfigureServices(sc);
            //var provider = sc.BuildServiceProvider();
            //// This will succeed.
            //var asampleClient = provider.GetService<IASampleRabbitMqClient>();

            //var portServices2 = new SendSerialPortServices();
            //portServices2.Read();

            #region mq
            asampleClient.Subscribe(result =>
            {
                Console.WriteLine("开始打印登机牌");
                
                //send  result to SerialPort
                var portServices = new SendSerialPortServices();
                portServices.Wirte(result);
            });
            #endregion
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();
        }
    }
}
