using System.Xml.Serialization;

namespace ASample.NetCore.WeChat.WeChatRedPack
{
    [XmlRoot("xml")]
    public class RedPackResult
    {
        /// <summary>
        /// 返回状态码(SUCCESS/FAIL 此字段是通信标识，非红包发放结果标识，红包发放是否成功需要查看result_code来判断)
        /// </summary>
        [XmlElement("return_code")]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 返回信息()
        /// </summary>
        [XmlElement("return_msg")]
        public string ReturnMsg { get; set; }

        //以下字段在return_code为SUCCESS的时候有返回
        /// <summary>
        /// 业务结果(SUCCESS/FAIL注意：当状态为FAIL时，存在业务结果未明确的情况。所以如果状态是FAIL，请务必再请求一次查询接口[请务必关注错误代码（err_code字段），通过查询得到的红包状态确认此次发放的结果。]，以确认此次发放的结果。)
        /// </summary>
        [XmlElement("result_code")]
        public string ResultCode { get; set; }


        /// <summary>
        /// 错误代码
        /// </summary>
        [XmlElement("err_code")]
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        [XmlElement("err_code_des")]
        public string ErrCodeDes { get; set; }


        //以下字段在return_code和result_code都为SUCCESS的时候有返回

        /// <summary>
        /// 商户订单号
        /// </summary>
        [XmlElement("mch_billno")]
        public string MchBillno { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [XmlElement("mch_id")]
        public string MchId { get; set; }

        /// <summary>
        /// 公众账号appid
        /// </summary>
        [XmlElement("wxappid")]
        public string WxAppId { get; set; }

        /// <summary>
        /// 用户openid
        /// </summary>
        [XmlElement("re_openid")]
        public string ReOpenId { get; set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        [XmlElement("total_amount")]
        public string TotalAmount { get; set; }

        /// <summary>
        /// 微信单号
        /// </summary>
        [XmlElement("send_listid")]
        public string SendListid { get; set; }

    }
}
