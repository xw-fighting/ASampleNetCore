using ASample.NetCore.Auths.Domains;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ASample.NetCore.Auths.Models.Rights
{

    public class RightDto
    {
        public Guid Id { get; set; }
        public string RightName { get; set; }
        public string RightIcon { get; set; }
        public string RightUrl { get; set; }
        public List<RightDto> SubRights { get; set; }
    }

    public class MenuResult
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public List<MenuDto> Data { get; set; }
    }


    public class MenuDto
    {
       // public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("jump")]
        public string Jump { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("list")]
        public List<MenuDto> List { get; set; }
    }

    public class RightComparer : IEqualityComparer<TRight>
    {
        public bool Equals(TRight x, TRight y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(TRight obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    public class MenuComparer : IEqualityComparer<TRight>
    {
        public bool Equals(TRight x, TRight y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(TRight obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
