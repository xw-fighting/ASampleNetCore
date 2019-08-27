using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.IdentityServers.Services
{
    public class ClientService:IClientService
    {
        private readonly IClientStore _clientStore;

        public ClientService(IClientStore clientStore)
        {
            _clientStore = clientStore;
        }

        public async Task<Client> GetClientAsync(string clientId)
        {
            var client = await _clientStore.FindClientByIdAsync(clientId);
            return client;
        }
    }
}
