using ASample.NetCore.SqlServerDb.Repository;
using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.EntityFramwork;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public class SqlServerUserRepository : SqlServerRepository<ASampleSqlServerDbContext,User>, ISqlServerUserRepository
    {
        private readonly IUnitOfWork<ASampleSqlServerDbContext> _unitOfWork;
        public SqlServerUserRepository(IUnitOfWork<ASampleSqlServerDbContext> unitOfWork):base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
