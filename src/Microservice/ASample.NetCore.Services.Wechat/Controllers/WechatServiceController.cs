using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASample.NetCore.Http;
using ASample.NetCore.Serialize;
using ASample.NetCore.WeChat;
using ASample.NetCore.WeChat.WeChatEvent;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.WechatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeChatServiceController : ControllerBase
    {
        private readonly IWeChatAuthService _weChatAuthService;
        private readonly IWeChatPayService _weChatPayService;
        private readonly IWeChatMessageService _wChatMessageService;
        private readonly IWeChatRedPackService _wxSendCashRedPackService;
        private readonly IWeChatEventServices _weChatEventServices;

        public WeChatServiceController(IWeChatAuthService weChatAuthService, 
            IWeChatPayService weChatPayService, 
            IWeChatMessageService wChatMessageService,
            IWeChatRedPackService wxSendCashRedPackService,
            IWeChatEventServices weChatEventServices)
        {
            _weChatAuthService = weChatAuthService;
            _weChatPayService = weChatPayService;
            _wChatMessageService = wChatMessageService;
            _wxSendCashRedPackService = wxSendCashRedPackService;
            _weChatEventServices = weChatEventServices;
        }
        [HttpGet("gateway")]
        public async Task<string> WxGateWay([FromQuery]VerifyGateWayParam param)
        {
            var result = await _weChatEventServices.VerifyGateway(param);
            if (result)
                return param.Echostr;
            else
                return string.Empty;
        }

        [HttpPost("gateway")]
        public async Task<string> WxGateWay()
        {
            string content = null;
            using (var reader = new StreamReader(Request.Body))
            {
                content = reader.ReadToEnd();
            }
            var xmlD = new XmlSerialize();
            var param = xmlD.Deserialize<SubscribeEventParam>(content);
            switch (param.Event)
            {
                case "subscribe":
                    //SubscribeEventHandler(()=> { });
                    break;
                default:
                    break;
            }
            return "success";
        }

        // GET api/values
        [HttpGet("accesstoken")]
        public async Task<HttpRequestResult> GetAccessToken()
        {
            var result = await _weChatAuthService.GetAccessTokenAsync();
            return result;
        }

        [HttpGet("jsapiconfig")]
        public async Task<HttpRequestResult> GetJsApiConfig()
        {
            var result = await _weChatAuthService.GetJsApiConfigAsync();
            return result;
        }

        [HttpGet("createmenu")]
        public async Task<HttpRequestResult> CreateWxMenu()
        {
            string menuJson;
            var path = AppDomain.CurrentDomain.BaseDirectory;
            using (var fs = new FileStream(Path.Combine(path,"menuJson.json"), FileMode.Open))
            {
                using (var sr = new StreamReader(fs, Encoding.GetEncoding("UTF-8")))
                {
                    menuJson = sr.ReadToEnd();
                }
            }
            var result = await _weChatAuthService.CreateMenuAsync(menuJson);
            return result;
        }

        [HttpGet("baseuserinfo/{openId}")]
        public async Task<HttpRequestResult> GetWxBaseUserInfo([FromRoute]string openId)
        {
            var result = await _weChatAuthService.GetBasicInfoAsync(openId);
            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
