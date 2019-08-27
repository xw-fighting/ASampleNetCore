using IdentityServer4.Models;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.IdentityServers.Services
{
    public interface IClientService
    {
        Task<Client> GetClientAsync(string clientId);
    }
}
