using System;
using System.Threading.Tasks;
using ASample.NetCore.MongoDb.Repository;
using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.EntityFramwork;

namespace ASample.NetCore.DbApiTest.Repositories
{

    public class MongoUserRepository : IMongoUserRepository
    {
        private readonly  IMongoRepository<User> _mongoDbRepository;

        public MongoUserRepository(IMongoRepository<User> mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        public async Task AddAsync(User user)
        {
            await _mongoDbRepository.AddAsync(user);
        }

        public async Task<User> GetAsync(Guid id)
        {
            var result =await _mongoDbRepository.GetAsync(c => c.Id == id);
            return result;
        }

        public async Task UpdateAsync(User user)
        {

            await _mongoDbRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _mongoDbRepository.DeleteAsync(id);
        }
    }
}
