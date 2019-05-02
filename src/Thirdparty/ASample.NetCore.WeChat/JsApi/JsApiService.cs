using ASample.NetCore.Http;
using ASample.NetCore.WeChat.Models.JsApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASample.NetCore.WeChat.JsApi
{
    public class JsApiService
    {
        public async Task<JsApiResult> GetJsApiTicket()
        {
            var accessTokenResult = await AccessTokenService.GetAccessToken();
            var url = $"https://api.weixin.qq.com/cgi-bin/ticket/getticket?type=jsapi&access_token={accessTokenResult.AccessToken}";
            var response = await HttpClientService.GetAsync(url);
            var result = new JsApiResult();
            if (response.IsError)
                 result = JsonConvert.DeserializeObject<JsApiResult>(response.Data);
            return result;
        }
    }
}
