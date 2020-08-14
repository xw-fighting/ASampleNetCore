using System.Threading.Tasks;
using ASample.NetCore.NonRelationalDb;
using ASample.NetCore.Services.IdentityServers.Domain;
using IdentityServer4.Models;

namespace ASample.NetCore.Services.IdentityServers.Repositories
{
    public interface IClientRepository :IRepository<ClientsEntity>
    {
        Task<Client> FindClientByIdAsync(string clientId);
    }
}
