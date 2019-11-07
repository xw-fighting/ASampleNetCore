using System;
using System.Threading.Tasks;
using ASample.NetCore.RelationalDb;
using ASample.NetCore.WebApi.Domain;

namespace ASample.NetCore.WebApi.Repositories.User
{

    public class SqlUserInfoRepository : ISqlUserInfoRepository
    {
        private readonly  IRepository<UserInfo> _sqlRepository;
        public SqlUserInfoRepository(IRepository<UserInfo> sqlRepository)
        {
            _sqlRepository = sqlRepository;
        }

        public async Task AddAsync(UserInfo user)
        {
            await _sqlRepository.AddAsync(user);
        }

        public async Task<UserInfo> GetAsync(Guid id)
        {
            var result =await _sqlRepository.GetAsync(id);
            return result;
        }
    }
}
