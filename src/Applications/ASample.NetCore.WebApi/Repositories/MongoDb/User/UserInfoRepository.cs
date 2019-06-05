using System;
using System.Threading.Tasks;
using ASample.NetCore.MongoDb.Repository;
using ASample.NetCore.WebApi.Domain;

namespace ASample.NetCore.WebApi.Repositories.User
{

    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly  IMongoRepository<UserInfo> _mongoDbRepository;
        public UserInfoRepository(IMongoRepository<UserInfo> mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        public async Task AddAsync(UserInfo user)
        {
            await _mongoDbRepository.AddAsync(user);
        }

        public async Task<UserInfo> GetAsync(Guid id)
        {
            var result =await _mongoDbRepository.GetAsync(id);
            return result;
        }
    }
}
