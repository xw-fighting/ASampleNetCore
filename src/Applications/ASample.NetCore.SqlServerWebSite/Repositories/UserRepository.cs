using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.SqlServer.Repository;
using ASample.NetCore.SqlServerWebSite.Domain;

namespace ASample.NetCore.SqlServerWebSite.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ISqlServerRepository<User> _sqlServerRepository;
        public UserRepository(ISqlServerRepository<User> sqlServerRepository)
        {
            _sqlServerRepository = sqlServerRepository;
        }
        public async Task AddAsync(User user)
        {
            await _sqlServerRepository.AddAsync(user);
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await _sqlServerRepository.GetAsync(c => c.Id == id);
            return user;
        }
    }
}
