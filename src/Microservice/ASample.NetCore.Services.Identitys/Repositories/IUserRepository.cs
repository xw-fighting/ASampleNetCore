using ASample.NetCore.Services.Identitys.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Identitys.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
