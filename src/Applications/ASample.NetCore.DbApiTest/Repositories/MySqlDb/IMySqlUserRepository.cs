using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.RelationalDb.Repositories;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public interface IMySqlUserRepository:IRepository<User>
    {

    }
}
