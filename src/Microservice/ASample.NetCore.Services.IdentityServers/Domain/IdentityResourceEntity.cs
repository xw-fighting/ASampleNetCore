using System.ComponentModel.DataAnnotations.Schema;
using ASample.NetCore.Domain;
using IdentityServer4.Models;
using Newtonsoft.Json;

namespace ASample.NetCore.Services.IdentityServers.Domain
{
    public class IdentityResourceEntity:AggregateRoot
    {
        public string IdentityResourceData { get; set; }

        public string IdentityResourceName { get; set; }

        [NotMapped]
        public IdentityResource IdentityResource { get; set; }

        public void AddDataToEntity()
        {
            IdentityResourceData = JsonConvert.SerializeObject(IdentityResource);
            IdentityResourceName = IdentityResource.Name;
        }

        public void MapDataFromEntity()
        {
            IdentityResource = JsonConvert.DeserializeObject<IdentityResource>(IdentityResourceData);
            IdentityResourceName = IdentityResource.Name;
        }
    }
}
