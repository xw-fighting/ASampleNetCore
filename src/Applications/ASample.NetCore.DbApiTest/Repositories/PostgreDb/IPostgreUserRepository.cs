using ASample.NetCore.DbApiTest.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public interface IPostgreUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);

        Task DeleteAsync(Guid id);
    }
}
