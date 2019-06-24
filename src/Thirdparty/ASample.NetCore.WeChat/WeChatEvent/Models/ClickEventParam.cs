using System.Xml.Serialization;

namespace ASample.NetCore.WeChat.WeChatEvent.Models
{
    /// <summary>
    /// 点击菜单拉取消息时的事件推送
    /// </summary>
    [XmlRoot("xml")]
    public class ClickEventParam:EventBaseParam
    {
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        [XmlElement("EventKey")]
        public string EventKey { get; set; }
    }
}
