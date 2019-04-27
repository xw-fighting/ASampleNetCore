using ASample.NetCore.Thirdparty.WeChat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASample.NetCore.Thirdparty.WeChat
{
    public class AccessTokenService
    {
        public static async Task<AccessTokenResult> GetAccessToken()
        {
            var url = "https://api.weixin.qq.com/cgi-bin/token";
            //grant_type=client_credential&appid=APPID&secret=APPSECRET
            //var result = 
            return null;
        }
    }
}
