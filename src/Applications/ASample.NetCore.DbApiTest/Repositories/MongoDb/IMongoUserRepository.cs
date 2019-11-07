using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.NonRelationalDb;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public interface IMongoUserRepository:IRepository<User>
    {

    }
}
