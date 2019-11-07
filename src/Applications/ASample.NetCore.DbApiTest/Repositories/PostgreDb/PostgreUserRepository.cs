using ASample.NetCore.EntityFramwork;
using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.RelationalDb;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public class PostgreUserRepository : Repository<ASamplePostgreDbContext,User>, IPostgreUserRepository
    {
        private IUnitOfWork<ASamplePostgreDbContext> _unitOfWork;
        public PostgreUserRepository(IUnitOfWork<ASamplePostgreDbContext> unitOfWork):base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
