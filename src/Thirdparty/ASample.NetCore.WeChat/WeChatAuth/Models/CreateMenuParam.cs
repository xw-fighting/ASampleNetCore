using Newtonsoft.Json;
using System.Collections.Generic;

namespace ASample.NetCore.WeChat.WeChatAuth
{

    public class CreateMenuParam
    {
        [JsonProperty("button")]
        public List<ButtonInfo> MenuButton { get; set; }
    }

    public class ButtonInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sub_button")]
        public List<ButtonInfo> SubButtonInfos { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    //public class ButtonInfo
    //{
    //    [JsonProperty("type")]
    //    public string Type { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("url")]
    //    public string Url { get; set; }
    //}
}
