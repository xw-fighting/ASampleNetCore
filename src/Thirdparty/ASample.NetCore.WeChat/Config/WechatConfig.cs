
namespace ASample.NetCore.WeChat
{
    public class WeChatConfig
    {
        /// <summary>
        /// 第三方用户唯一凭证
        /// </summary>
        public string WxAppId { get; set; }

        /// <summary>
        /// 第三方用户唯一凭证密钥，即appsecret
        /// </summary>
        public string WxSecret { get; set; }
        public string WxMechId { get; set; }

        /// <summary>
        /// 微信模板编号缩写
        /// </summary>
        public string WxMessageTemplateId { get; set; }

        /// <summary>
        /// 微信发送审核通知消息模板编号
        /// </summary>
        public string WxCheckMessageTemplateId { get; set; }
    }
}
