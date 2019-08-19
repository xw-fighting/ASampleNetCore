using ASample.NetCore.EntityFramwork;
using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.RelationalDb.Repositories;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public class PostgreUserRepository : Repository<AsamplePostgreDbContext,User>, IPostgreUserRepository
    {
        private IUnitOfWork<AsamplePostgreDbContext> _unitOfWork;
        public PostgreUserRepository(IUnitOfWork<AsamplePostgreDbContext> unitOfWork):base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
