using ASample.NetCore.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ASample.NetCore.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseLogging()
                .UseStartup<Startup>();
    }
}
