using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<AccessTokenResult> GetAccessToken()
        {
            var result = await AccessTokenService.GetAccessToken();
            return result;
        }
    }
}