using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.WeChat;
using ASample.NetCore.WeChat.Models;
using ASample.NetCore.WeChat.WeChatAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WeChatController : ControllerBase
    {
        public async Task<string> GetAccessToken()
        {
            var result = await WeChatAuthService.Current.GetAccessTokenAsync();
            return result;
        }
    }
}