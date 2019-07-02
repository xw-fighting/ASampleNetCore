using ASample.NetCore.SignalRWeb.Domains.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASample.NetCore.SignalRWeb.Repositories
{
    public interface IUserRepository
    {
        Task<UserInfo> GetAsync(Guid id);
        Task AddAsync(UserInfo user);
        Task <IEnumerable<UserInfo>> QueryAsync();
    }
}
