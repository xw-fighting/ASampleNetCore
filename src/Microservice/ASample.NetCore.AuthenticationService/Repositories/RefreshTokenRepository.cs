using System.Threading.Tasks;
using ASample.NetCore.AuthenticationService.Domain;
using ASample.NetCore.MongoDb.Repository;

namespace ASample.NetCore.AuthenticationService.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoRepository<RefreshToken> _repository;
        public RefreshTokenRepository(IMongoRepository<RefreshToken> repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(RefreshToken token)
        {
            await _repository.AddAsync(token);
        }

        public async Task<RefreshToken> GetAsync(string token)
        {
            var result = await _repository.GetAsync(t => t.Token == token);
            return result;
        }

        public async Task UpdateAsync(RefreshToken token)
        {
            await _repository.UpdateAsync(token);
        }
    }
}
