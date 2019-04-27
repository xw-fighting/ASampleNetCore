using ASample.NetCore.ConfigReaders;
using ASample.NetCore.HttpClients;
using ASample.NetCore.WeChat.Config;
using ASample.NetCore.WeChat.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASample.NetCore.WeChat
{
    public class AccessTokenService
    {
        public static async Task<AccessTokenResult> GetAccessToken()
        {
            ConfigReader.FileName = "wechatconfig.json";
            var url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential";
            var wechatConfig = ConfigReader.GetValue<WechatConfig>("wechatconfig");
            var respose = await HttpClientService.GetAsync<HttpRequestResult>($"{url}&{wechatConfig.AppId}&{wechatConfig.Secret}");
            if (respose.IsError)
                return null;
            var result = JsonConvert.DeserializeObject<AccessTokenResult>(respose.Data);
            return result;
        }
    }
}
