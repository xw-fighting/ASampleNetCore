using ASample.NetCore.DbApiTest.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public interface ISqlServerUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task AddAsync(User user);
    }
}
