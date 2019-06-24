
using Newtonsoft.Json;

namespace ASample.NetCore.WeChat.WeChatAuth
{
    /// <summary>
    /// 输出结果参数
    /// </summary>
    public class CreateMenuResult
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        [JsonProperty("errcode")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("errmsg")]
        public string ErrorMsg { get; set; }
    }
}
