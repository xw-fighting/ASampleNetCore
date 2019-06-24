using System.ComponentModel;

namespace ASample.NetCore.Http
{
    public enum DeserializeType
    {
        [Description("RawString")]
        Default = 0,
        [Description("Json 序列化")]
        JsonDeserialize = 1,
        [Description("Xml 序列化")]
        XmlDeserialize = 2
    }
}
