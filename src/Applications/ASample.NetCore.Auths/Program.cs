using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace ASample.NetCore.Auths
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            //   .MinimumLevel.Verbose()
            //   .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            //   //.Enrich.With(new LogEnricher())
            //   .WriteTo.Seq("http://localhost:5341", apiKey: "pGU0mvbfEfCz28cJY5a5")
            //   .CreateLogger();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //.UseSerilog()
            .UseStartup<Startup>();
    }
}
