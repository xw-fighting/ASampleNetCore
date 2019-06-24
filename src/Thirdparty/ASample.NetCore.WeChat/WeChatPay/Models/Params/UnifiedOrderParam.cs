using System;
using System.Collections.Generic;
using System.Text;

namespace ASample.NetCore.WeChat.WeChatPay
{
    public class UnifiedOrderParam
    {
        public string Body { get; set; }
        public string TotalFee { get; set; }
        public string OutTradeNo { get; set; }
        public string NotifyUrl { get; set; }
        public string TradeType { get; set; }
        public string OpenId { get; set; }

    }
}
