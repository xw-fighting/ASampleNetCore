using System.Xml.Serialization;

namespace ASample.NetCore.WeChat.WeChatEvent.Models
{
    /// <summary>
    /// 点击菜单跳转链接时的事件推送
    /// </summary>
    [XmlRoot("xml")]
    public class ViewEventParam:EventBaseParam
    {
        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        [XmlElement("EventKey")]
        public string EventKey { get; set; }
    }
}
