using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.MySqlDb.Repositories;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public interface IMySqlUserRepository:IMySqlRepository<User>
    {

    }
}
