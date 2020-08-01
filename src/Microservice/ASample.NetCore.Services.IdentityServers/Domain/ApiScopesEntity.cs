using System.ComponentModel.DataAnnotations.Schema;
using ASample.NetCore.Domain;
using IdentityServer4.Models;
using Newtonsoft.Json;

namespace ASample.NetCore.Services.IdentityServers.Domain
{
    public class ApiScopesEntity:AggregateRoot
    {
        public string ApiScopeData { get; set; }

        public string ApiScopeName { get; set; }

        [NotMapped]
        public ApiScope ApiScope { get; set; }

        public void AddDataToEntity()
        {
            ApiScopeData = JsonConvert.SerializeObject(ApiScope);
            ApiScopeName = ApiScope.Name;
        }

        public void MapDataFromEntity()
        {
            ApiScope = JsonConvert.DeserializeObject<ApiScope>(ApiScopeData);
            ApiScopeName = ApiScope.Name;
        }
    }
}
