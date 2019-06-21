using ASample.NetCore.Configuration;
using ASample.NetCore.Http;
using ASample.NetCore.WeChat.Core;
using ASample.NetCore.WeChat.Models;
using ASample.NetCore.WeChat.Models.JsApi;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace ASample.NetCore.WeChat
{
    public class WeChatAuthService: IWeChatAuthService
    {
        private readonly MemoryCache _cache;
        private readonly string _wechatKey = "wechat_accesstoken";
        private readonly IOptions<WechatOptions> _wechatOption;
        public WeChatAuthService(IOptions<WechatOptions> options)
        {
            _wechatOption = options;
            _cache = MemoryCache.Default;
        }
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<HttpRequestResult> GetBasicInfoAsync(string openId)
        {
            var accessTokenResult = await GetAccessTokenAsync();
            if (accessTokenResult.IsError)
                return HttpRequestResult.Error(accessTokenResult.Message);
            var accessToken = accessTokenResult.Data.ToString();
            var url = $"https://api.weixin.qq.com/cgi-bin/user/info?access_token={accessToken}&openid={openId}&lang=zh_CN";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var respStr = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WxUserInfoResult>(respStr);
            return HttpRequestResult.Success<WxUserInfoResult>(result, "");
        }
        /// <summary>
        /// 获取微信accesstoken, 优先从缓存获取，其次重新请求获取
        /// </summary>
        /// <returns></returns>
        public async Task<HttpRequestResult> GetAccessTokenAsync()
        {
            var accessToken = _cache.Get(_wechatKey) as string;
            if (!string.IsNullOrWhiteSpace(accessToken))
                return HttpRequestResult.Success(accessToken, "");
            var result = await RequestAccessTokenAsync();
            _cache.Remove(_wechatKey);
            if (string.IsNullOrWhiteSpace(result.AccessToken))
                return HttpRequestResult.Error("AccessToken is null");
            _cache.Add(new CacheItem(_wechatKey, result.AccessToken),
                new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(result.ExpireTime - 10)
                }); //在过期时间(秒) 减去10秒，冗余
            return HttpRequestResult.Success(result.AccessToken, "");
        }
        /// <summary>
        /// 微信分享时获取配置参数
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<HttpRequestResult> GetJsApiConfigAsync(string url)
        {
            var requestResult = await GetJsApiTicketAsync();
            if (requestResult.IsError)
                return HttpRequestResult.Error(requestResult.Message);
            var jsapiResult = (JsApiResult)requestResult.Data;

            var dic = new SortedDictionary<string, object>();
            var noncestr = WeChatPayUtility.GeneratorNonceStr();
            var timestamp = WeChatPayUtility.GenerateTimeStamp();

            dic.SetValue("noncestr", noncestr);
            dic.SetValue("timestamp", timestamp);
            dic.SetValue("url", url);
            dic.SetValue("jsapi_ticket", jsapiResult.Ticket);

            var shaStr = dic.ToUrlString();
            var signature = WeChatPayUtility.MakeSign(shaStr);
            var result = new JsApiConfigResult
            {
                AppId = _wechatOption.Value.WxAppId,
                TimeStamp = timestamp,
                NonceStr = noncestr,
                Signature = signature,
            };
            return HttpRequestResult.Success(result,"");
        }

        /// <summary>
        /// 获取 JS_Ticket
        /// </summary>
        /// <returns></returns>
        public async Task<HttpRequestResult> GetJsApiTicketAsync()
        {
            var accessTokenResult = await GetAccessTokenAsync();
            var url = $"https://api.weixin.qq.com/cgi-bin/ticket/getticket?type=jsapi&access_token={accessTokenResult}";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var responseStr = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<JsApiResult>(responseStr);
            if (result.ErrCode == "0")
                return HttpRequestResult.Success<JsApiResult>(result,"");
            return  HttpRequestResult.Error(result.ErrMsg);
        }

        /// <summary>
        /// 创建菜单按钮
        /// </summary>
        /// <param name="menuJsonStr"></param>
        /// <returns></returns>
        public  async Task<HttpRequestResult> CreateMenuAsync(string menuJsonStr)
        {
            //获取微信令牌
            var accessToken = await GetAccessTokenAsync();

            //创建菜单
            var createMenuUrl = $"https://api.weixin.qq.com/cgi-bin/menu/create?access_token={accessToken}";
            var result = await PostRequestAsync<CreateMenuResult>(createMenuUrl, menuJsonStr);
            if (result.ErrorCode == "0")
                return HttpRequestResult.Success<CreateMenuResult>(result,"");
            return HttpRequestResult.Error(result.ErrorMsg);
        }

        /// <summary>
        /// post方法访问
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        private static async Task<T> PostRequestAsync<T>(string postUrl, string postData)
        {
            // 设置参数
            var httpClient = new HttpClient();
            var content = new StringContent(postData);
            var response = await httpClient.PostAsync(postUrl, content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(jsonResult);
            return result;
        }

        /// <summary>
        /// 请求微信服务器，以获取accesstoken信息
        /// 文档地址 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140183
        /// </summary>
        /// <returns></returns>
        private async Task<AccessTokenResult> RequestAccessTokenAsync()
        {
            //var config = ConfigurationReader.GetValue<WechatOptions>("wechatconfig");
            var wxAppId = _wechatOption.Value.WxAppId;
            var wxSecert = _wechatOption.Value.WxSecret;
            var url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={wxAppId}&secret={wxSecert}";
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(url, null);
            var respStr = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AccessTokenResult>(respStr);
            return result;
        }

        public void Dispose()
        {
            _cache?.Dispose();
        }
    }
}
