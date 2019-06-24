
namespace ASample.NetCore.WeChat.WeChatRedPack
{
    public class QueryRedPackRecordParam
    {
        /// <summary>
        /// 
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 商户发放红包的商户订单号
        /// </summary>
        public string MchBillNo { get; set; }

        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 微信分配的公众账号ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// MCHT:通过商户订单号获取红包信息。
        /// </summary>
        public string BillType { get; set; }
    }
}
