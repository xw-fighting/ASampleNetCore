using Newtonsoft.Json;

namespace ASample.NetCore.WeChat.Models
{
    public class RefundParam
    {
        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }

        [JsonProperty("total_fee")]

        public string TotalFee { get; set; }

        [JsonProperty("refund_fee")]

        public string RefundFee { get; set; }
    }
}
