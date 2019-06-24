using Newtonsoft.Json;

namespace ASample.NetCore.WeChat.WeChatMessage
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
