using Newtonsoft.Json;

namespace ASample.NetCore.WeChat.Models
{
    public class SendMsgParameter<T> : MsgTemplateBasicParameter where T : MsgTemplateDataBasicParameter
    {
        /// <summary>
        /// 消息参数
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
