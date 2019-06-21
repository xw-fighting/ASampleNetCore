using Newtonsoft.Json;

namespace ASample.NetCore.WeChat.Models
{
    public class AccessTokenResult
    {
        /// <summary>
        /// 令牌
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpireTime { get; set; }
        
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("errcode")]
        public string ErrorCode { get; set; }
        
        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("errmsg")]
        public string ErrorMsg { get; set; }
    }
}
