using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ASample.NetCore.WeChat.WeChatEvent;
using Microsoft.Extensions.Options;

namespace ASample.NetCore.WeChat
{
    public class WeChatEventServices : IWeChatEventServices
    {
        private readonly WechatOptions _options;

        public WeChatEventServices(IOptions<WechatOptions> options)
        {
            _options = options.Value;
        }

        public Task<EventBaseParam> GetWeChatEventType()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VerifyGateway(VerifyGateWayParam param)
        {
            var list = new List<string>()
            {
                _options.Token, //SignatureParameter.Token,
                param.TimeStamp,
                param.Nonce
            };
            list.Sort();//排序
            var input = string.Join("", list);
            var newSignature = WeChatPayUtility.SHA1_Encrypt(input);
            return param.Signature == newSignature;
        }
    }
}
