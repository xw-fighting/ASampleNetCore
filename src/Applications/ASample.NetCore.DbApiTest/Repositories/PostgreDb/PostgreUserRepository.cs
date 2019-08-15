using ASample.NetCore.EntityFramwork;
using ASample.NetCore.PostgreDb.Repositories;
using ASample.NetCore.DbApiTest.Domain;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public class PostgreUserRepository : PostgreRepository<AsamplePostgreDbContext,User>, IPostgreUserRepository
    {
        private IUnitOfWork<AsamplePostgreDbContext> _unitOfWork;
        public PostgreUserRepository(IUnitOfWork<AsamplePostgreDbContext> unitOfWork):base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
