using ASample.NetCore.Domain.AggregateRoots;


namespace ASample.NetCore.Services.IdentityServers.Domain
{
    public class Clients:AggregateRoot
    {
        public string ClientId { get; set; }
        public string ClientSecrets { get; set; }
    }
}
