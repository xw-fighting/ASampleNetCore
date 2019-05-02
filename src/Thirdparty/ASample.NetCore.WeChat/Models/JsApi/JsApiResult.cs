using Newtonsoft.Json;

namespace ASample.NetCore.WeChat.Models.JsApi
{
    public class JsApiResult
    {
        [JsonProperty("errcode")]
        public string ErrCode { get; set; }

        [JsonProperty("errmsg")]

        public string ErrMsg { get; set; }

        [JsonProperty("ticket")]

        public string Ticket { get; set; }

        [JsonProperty("expires_in")]

        public string ExpiresIn { get; set; }
    }
}
