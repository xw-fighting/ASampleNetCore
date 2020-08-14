using IdentityServer4.Models;
using System.Threading.Tasks;
using ASample.NetCore.Services.IdentityServers.Repositories;

namespace ASample.NetCore.Services.IdentityServers.Services
{
    public class ClientService:IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> GetClientAsync(string clientId)
        {
            var client = await _clientRepository.FindClientByIdAsync(clientId);
            return client;
        }
    }
}
