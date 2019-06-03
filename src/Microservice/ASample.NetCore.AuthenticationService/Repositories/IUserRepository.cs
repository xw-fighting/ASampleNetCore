using ASample.NetCore.AuthenticationService.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.AuthenticationService.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
