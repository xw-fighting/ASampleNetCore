using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.SqlServerDb.Repository;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public interface ISqlServerUserRepository:ISqlServerRepository<User>
    {

    }
}
