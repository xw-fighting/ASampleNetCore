using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Http;
using ASample.NetCore.WeChat;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.WechatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WechatServiceController : ControllerBase
    {
        private readonly IWeChatAuthService _weChatAuthService;
        private readonly IWeChatPayService _weChatPayService;
        private readonly IWeChatMessageService _wChatMessageService;

        public WechatServiceController(IWeChatAuthService weChatAuthService, IWeChatPayService weChatPayService, IWeChatMessageService wChatMessageService)
        {
            _weChatAuthService = weChatAuthService;
            _weChatPayService = weChatPayService;
            _wChatMessageService = wChatMessageService;
        }

        // GET api/values
        [HttpGet("accesstoken")]
        public async Task<HttpRequestResult> GetAccessToken()
        {
            var result = await _weChatAuthService.GetAccessTokenAsync();
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
