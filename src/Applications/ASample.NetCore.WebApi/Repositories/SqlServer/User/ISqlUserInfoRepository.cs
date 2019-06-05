using ASample.NetCore.WebApi.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.WebApi.Repositories.User
{
    public interface ISqlUserInfoRepository
    {
        Task<UserInfo> GetAsync(Guid id);
        Task AddAsync(UserInfo user);
    }
}
