using ASample.NetCore.Startup;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace ASample.NetCore.WeChat.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
   
    [TestClass]
    public class AccessToken
    {
        public  IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args);
        [TestMethod]
        public void GetAccessToken()
        {
            var host = new WebHostBuilder()
                .UseStartup<StartupConfig>()
             .Build();
            host.Run();

            var result = AccessTokenService.GetAccessToken().GetAwaiter().GetResult();
            Assert.IsTrue(result.AccessToken != null);
                
        }
    }
}
