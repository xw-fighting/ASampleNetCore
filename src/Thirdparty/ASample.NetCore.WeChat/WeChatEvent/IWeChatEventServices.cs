using ASample.NetCore.WeChat.WeChatEvent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASample.NetCore.WeChat
{
    public interface  IWeChatEventServices
    {
        Task<bool> VerifyGateway(VerifyGateWayParam param);

        Task<EventBaseParam> GetWeChatEventType();
    }
}
