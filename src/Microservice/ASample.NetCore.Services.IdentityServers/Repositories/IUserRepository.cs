using ASample.NetCore.NonInertialDb.Repositories;
using ASample.NetCore.Services.IdentityServers.Domain;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.IdentityServers.Repositories
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
    }
}
