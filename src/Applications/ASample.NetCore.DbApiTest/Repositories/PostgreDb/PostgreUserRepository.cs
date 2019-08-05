using ASample.NetCore.EntityFramwork;
using ASample.NetCore.PostgreDb.Repositories;
using ASample.NetCore.DbApiTest.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public class PostgreUserRepository : IPostgreUserRepository
    {
        private IPostgreRepository<User> _postgreRepository;
        private IUnitOfWork<AsamplePostgreDbContext> _unitOfWork;
        public PostgreUserRepository(IPostgreRepository<User> postgreRepository, IUnitOfWork<AsamplePostgreDbContext> unitOfWork)
        {
            _postgreRepository = postgreRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(User user)
        {
            await _postgreRepository.AddAsync(user);
            _unitOfWork.SaveChanges();
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await _postgreRepository.GetAsync(c => c.Id == id);
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            await _postgreRepository.UpdateAsync(user);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _postgreRepository.DeleteAsync(id);
            _unitOfWork.SaveChanges();
        }
    }
}
