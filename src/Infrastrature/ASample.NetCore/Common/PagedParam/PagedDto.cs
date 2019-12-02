using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace ASample.NetCore.Common
{
    public class PagedDto<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("data")]
        public List<T> Data { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 10;

        [JsonProperty("totalPage")]
        public long TotalPage => (Total / PageSize);

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
