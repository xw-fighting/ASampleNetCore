using System.Xml.Serialization;

namespace ASample.NetCore.WeChat.WeChatEvent.Models
{
    [XmlRoot("xml")]
    public class LocationEventParam:EventBaseParam
    {
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        [XmlElement("Latitude")]
        public string Latitude { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        [XmlElement("Longitude")]
        public string Longitude { get; set; }

        /// <summary>
        /// 地理位置精度
        /// </summary>
        [XmlElement("Precision")]
        public string Precision { get; set; }
    }
}
