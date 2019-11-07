using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.RelationalDb;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public class SqlServerUserRepository : Repository<ASampleSqlServerDbContext,User>, ISqlServerUserRepository
    {
        private readonly IUnitOfWork<ASampleSqlServerDbContext> _unitOfWork;
        public SqlServerUserRepository(IUnitOfWork<ASampleSqlServerDbContext> unitOfWork):base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
