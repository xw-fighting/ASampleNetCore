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
        public string ExpiresIn { get; set; }
    }
}
