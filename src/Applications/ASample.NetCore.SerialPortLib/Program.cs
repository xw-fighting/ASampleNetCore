using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Serilog;

namespace ASample.NetCore.SerialPortLib
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) 
        {
            var port = ConfigurationReader.GetValue("HangfirePort");
            return WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseKestrel(opt => {
                    opt.ListenLocalhost(Convert.ToInt32(port));
                })
                .UseStartup<Startup>();
        }
    }
}
