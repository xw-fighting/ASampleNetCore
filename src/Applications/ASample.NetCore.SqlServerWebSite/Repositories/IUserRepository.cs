using ASample.NetCore.EntityFramwork;
using ASample.NetCore.SqlServerWebSite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.SqlServerWebSite.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task AddAsync(User user);
    }
}
