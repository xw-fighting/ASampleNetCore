using ASample.NetCore.EntityFramwork;
using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.RelationalDb;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public class MySqlUserRepository: Repository<ASampleMySqlDbContext,User>,IMySqlUserRepository
    {
        private IUnitOfWork<ASampleMySqlDbContext> _unitOfWork;
        public MySqlUserRepository(IUnitOfWork<ASampleMySqlDbContext> unitOfWork):base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
    }
}
