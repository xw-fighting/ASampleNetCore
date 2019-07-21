using Newtonsoft.Json;
using System.Collections.Generic;

namespace ASample.NetCore.Common
{
    public class LayuiTableDto<T>
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}
