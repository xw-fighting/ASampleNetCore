using ASample.NetCore.WeChat.WeChatMessage;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ASample.NetCore.WeChat
{
    public class WeChatMessageService:IWeChatMessageService
    {
        private readonly IWeChatAuthService _weChatAuthService;

        public WeChatMessageService(IWeChatAuthService weChatAuthService)
        {
            _weChatAuthService = weChatAuthService;
        }
        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <typeparam name="TData">模板类</typeparam>
        /// <param name="openId">用户的openId</param>
        /// <param name="templateId">模板编号<see cref="MsgTemplateIds"/></param>
        /// <param name="data">模板参数</param>
        /// <returns></returns>
        public async Task<SendMsgResult> SendTemplateMsgAsync<TData>(string openId, string templateId, TData data) where TData : MsgTemplateDataBasicParameter
        {
            //获取访问微信接口的access_token
            var accessToken = await _weChatAuthService.GetAccessTokenAsync();
            //微信发送模板消息url,
            var url = $"https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={accessToken}";
            //参数
            var sendMsgParameter = new SendMsgParameter<TData>
            {
                ToUser = openId,
                Url = "http://weixin.qq.com/download",
                TemplateId = templateId,
                Data = data
            };

            var result = await SendMsgToWechatAsync(url, sendMsgParameter);
            return result;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<SendMsgResult> SendMsgToWechatAsync<TData>(string url, SendMsgParameter<TData> param) where TData : MsgTemplateDataBasicParameter
        {
            //记录日志
            //var log = Logger?.CreateChildLogger("wechat");

            var strContent = JsonConvert.SerializeObject(param);
            var content = new StringContent(strContent);
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(url, content);
            var resultStr = await response.Content.ReadAsStringAsync();

            //将发送微信消息的结果记录都日志
            //log?.Debug("return string" + resultStr);

            var result = JsonConvert.DeserializeObject<SendMsgResult>(resultStr);
            return result;
        }


        public async Task<bool> SendTextMsgAsync(string msg)
        {
            return true;
        }
    }
}
