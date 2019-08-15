using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.PostgreDb.Repositories;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public interface IPostgreUserRepository: IPostgreRepository<User>
    {

    }
}
