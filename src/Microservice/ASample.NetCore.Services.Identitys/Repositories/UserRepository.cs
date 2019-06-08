using ASample.NetCore.Services.Identitys.Domain;
using ASample.NetCore.MongoDb.Repository;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Identitys.Repositories
{
    public class UserRepository :IUserRepository
    {
        private readonly IMongoRepository<User> _repository;
        public UserRepository(IMongoRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(User user)
        {
            await _repository.AddAsync(user);
        }

        public async Task<User> GetAsync(Guid id)
        {
            var result = await _repository.GetAsync(id);
            return result;
        }

        public async Task<User> GetAsync(string email)
        {
            var result = await _repository.GetAsync(u => u.Email == email.ToLowerInvariant());
            return result;
        }

        public async Task UpdateAsync(User user)
        {
            await _repository.UpdateAsync(user);
        }
    }
}
