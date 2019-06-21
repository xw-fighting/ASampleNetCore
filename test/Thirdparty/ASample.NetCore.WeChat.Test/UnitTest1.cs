using ASample.NetCore.Startup;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
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
        [TestMethod]
        public void GetAccessToken()
        {
            //IServiceCollection services = new ServiceCollection();
            //services.AddHttpClient();
            //var result = WeChatAuthService.Current.GetAccessTokenAsync().GetAwaiter().GetResult();
            //Assert.IsTrue(result != null);
                
        }
    }
}
