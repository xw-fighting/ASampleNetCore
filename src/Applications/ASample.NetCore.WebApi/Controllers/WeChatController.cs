using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Http;
using ASample.NetCore.WeChat;
using ASample.NetCore.WeChat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WeChatController : ControllerBase
    {
        private readonly IWeChatAuthService _weChatAuthService;

        public WeChatController(IWeChatAuthService weChatAuthService)
        {
            _weChatAuthService = weChatAuthService;
        }
        public async Task<HttpRequestResult> GetAccessToken()
        {
            var result = await _weChatAuthService.GetAccessTokenAsync();
            return result;
        }
    }
}