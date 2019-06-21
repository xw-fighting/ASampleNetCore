using ASample.NetCore.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASample.NetCore.WeChat
{
    public static class Extension
    {
        private static readonly string SectionName = "wechat";

        public static void AddWeChatService(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            var section = configuration.GetSection(SectionName);
            var options = configuration.GetOptions<WechatOptions>(SectionName);
            services.Configure<WechatOptions>(section);
            services.AddSingleton(options);
            services.AddSingleton<IWeChatAuthService, WeChatAuthService>();
            services.AddSingleton<IWeChatPayService, WeChatPayService>();
            services.AddSingleton<IWeChatMessageService, WeChatMessageService>();
        }
    }
}
