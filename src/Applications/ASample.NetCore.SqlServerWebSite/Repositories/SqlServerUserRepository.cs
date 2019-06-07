using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.SqlServerDb.Repository;
using ASample.NetCore.SqlServerWebSite.Domain;
using ASample.NetCore.EntityFramwork;

namespace ASample.NetCore.SqlServerWebSite.Repositories
{
    public class SqlServerUserRepository : ISqlServerUserRepository
    {
        private ISqlServerRepository<User> _sqlServerRepository;
        private IUnitOfWork _unitOfWork;

        public SqlServerUserRepository(ISqlServerRepository<User> sqlServerRepository, IUnitOfWork unitOfWork)
        {
            _sqlServerRepository = sqlServerRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(User user)
        {
            await _sqlServerRepository.AddAsync(user);
            _unitOfWork.SaveChanges();
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await _sqlServerRepository.GetAsync(c => c.Id == id);
            return user;
        }
    }
}
