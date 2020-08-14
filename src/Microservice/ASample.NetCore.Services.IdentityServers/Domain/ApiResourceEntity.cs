using System.ComponentModel.DataAnnotations.Schema;
using ASample.NetCore.Domain;
using IdentityServer4.Models;
using Newtonsoft.Json;

namespace ASample.NetCore.Services.IdentityServers.Domain
{
    public class ApiResourceEntity:AggregateRoot
    {
        public string ApiResourceData { get; set; }

        public string ApiResourceName { get; set; }

        [NotMapped]
        public ApiResource ApiResource { get; set; }

        public void AddDataToEntity()
        {
            ApiResourceData = JsonConvert.SerializeObject(ApiResource);
            ApiResourceName = ApiResource.Name;
        }

        public void MapDataFromEntity()
        {
            ApiResource = JsonConvert.DeserializeObject<ApiResource>(ApiResourceData);
            ApiResourceName = ApiResource.Name;
        }
    }
}
