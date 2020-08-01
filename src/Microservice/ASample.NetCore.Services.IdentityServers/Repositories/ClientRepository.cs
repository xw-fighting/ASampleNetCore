using ASample.NetCore.EntityFramwork;
using ASample.NetCore.RelationalDb;
using ASample.NetCore.Services.IdentityServers.Domain;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ASample.NetCore.Services.IdentityServers.Repositories
{
    public class ClientRepository :Repository<ASampleSqlServerDbContext, ClientsEntity>, IClientRepository ,IClientStore
    {
        public ClientRepository(IUnitOfWork<ASampleSqlServerDbContext> unitOfWork) : base(unitOfWork)
        {
            ClientSet = unitOfWork.GetDbContext().Set<ClientsEntity>();
        }

        public DbSet<ClientsEntity> ClientSet { get; set; }
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = await ClientSet.FirstOrDefaultAsync(c => c.ClientId == clientId);
            var result = JsonConvert.DeserializeObject<Client>(client.ClientData);
            return result;
        }
    }
}
