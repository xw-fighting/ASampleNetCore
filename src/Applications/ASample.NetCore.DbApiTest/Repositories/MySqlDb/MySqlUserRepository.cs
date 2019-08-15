using ASample.NetCore.EntityFramwork;
using ASample.NetCore.MySqlDb.Repositories;
using ASample.NetCore.DbApiTest.Domain;
namespace ASample.NetCore.DbApiTest.Repositories
{
    public class MySqlUserRepository: MySqlRepository<ASampleMySqlDbContext,User>,IMySqlUserRepository
    {
        private IUnitOfWork<ASampleMySqlDbContext> _unitOfWork;
        public MySqlUserRepository(IUnitOfWork<ASampleMySqlDbContext> unitOfWork):base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
    }
}
