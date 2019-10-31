using ASample.NetCore.Domain;


namespace ASample.NetCore.Services.IdentityServers.Domain
{
    public class Clients:AggregateRoot
    {
        public string ClientId { get; set; }
        public string ClientSecrets { get; set; }
    }
}
