using ASample.NetCore.ConfigReaders;
using ASample.NetCore.Http;
using ASample.NetCore.WeChat.Config;
using ASample.NetCore.WeChat.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
            var getUrl = $"{url}&{wechatConfig.AppId}&{wechatConfig.Secret}";
            var respose = await HttpClientService.GetAsync(getUrl);
            if (respose.IsError)
                return null;
            var result = JsonConvert.DeserializeObject<AccessTokenResult>(respose.Data);
            return result;
        }

        public static async Task<AccessTokenResult> GetAccessToken2()
        {
            ConfigReader.FileName = "wechatconfig.json";
            var url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential";
            var wechatConfig = ConfigReader.GetValue<WechatConfig>("wechatconfig");
            var httpclient = new HttpClient();

            var resposeMes = await httpclient.GetAsync($"{url}&appid={wechatConfig.AppId}&secret={wechatConfig.Secret}");
            var resposeStr = await resposeMes.Content.ReadAsStringAsync();
            var respose = JsonConvert.DeserializeObject<HttpRequestResult>(resposeStr);
            if (respose.IsError)
                return null;
            var result = JsonConvert.DeserializeObject<AccessTokenResult>(respose.Data);
            return result;
        }
    }
}
