using System.ComponentModel.DataAnnotations.Schema;
using ASample.NetCore.Domain;
using IdentityServer4.Models;
using Newtonsoft.Json;

namespace ASample.NetCore.Services.IdentityServers.Domain
{
    public class ClientsEntity : AggregateRoot
    {
        public string ClientData { get; set; }

        public string ClientId { get; set; }

        [NotMapped]
        public Client Client { get; set; }

        public void AddDataToEntity()
        {
            ClientData = JsonConvert.SerializeObject(Client);
            ClientId = Client.ClientId;
        }

        public void MapDataFromEntity()
        {
            Client = JsonConvert.DeserializeObject<Client>(ClientData);
            ClientId = Client.ClientId;
        }
    }
}
