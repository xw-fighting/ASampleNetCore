using System.Xml.Serialization;

namespace ASample.NetCore.WeChat.WeChatEvent
{
    public class EventBaseParam
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        [XmlElement("ToUserName")]
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        [XmlElement("FromUserName")]
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        [XmlElement("CreateTime")]
        public string CreateTime { get; set; }

        /// <summary>
        /// 消息类型，event
        /// </summary>
        [XmlElement("MsgType")]
        public string MsgType { get; set; }

        /// <summary>
        /// 事件类型，subscribe(订阅)、unsubscribe(取消订阅)
        /// </summary>
        [XmlElement("Event")]
        public string Event { get; set; }
    }
}
