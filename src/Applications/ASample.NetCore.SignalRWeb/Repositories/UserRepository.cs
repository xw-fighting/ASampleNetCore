using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASample.NetCore.MongoDb.Repository;
using ASample.NetCore.SignalRWeb.Domains.Users;

namespace ASample.NetCore.SignalRWeb.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<UserInfo> _mongoDbRepository;
        public UserRepository(IMongoRepository<UserInfo> mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        public async Task AddAsync(UserInfo user)
        {
            await _mongoDbRepository.AddAsync(user);
        }

        public async Task<UserInfo> GetAsync(Guid id)
        {
            var result = await _mongoDbRepository.GetAsync(id);
            return result;
        }

        public async Task<IEnumerable<UserInfo>> QueryAsync()
        {
            var result = await _mongoDbRepository.QueryAsync(c => true);
            return result;
        }
    }
}
