using System;
using System.Threading.Tasks;
using ASample.NetCore.MongoDb.Repository;
using ASample.NetCore.Services.IdentityServers.Domain;

namespace ASample.NetCore.Services.IdentityServers.Repositories
{
    public class UserRepository : IUserRepository
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
            return  await _repository.GetAsync(id);
        }

        public async Task<User> GetAsync(string email)
        {
            return await _repository.GetAsync(c => c.Email == email.ToLowerInvariant());
        }

        public async Task UpdateAsync(User user)
        {
            await _repository.UpdateAsync(user);
        }
    }
}
