using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.NonInertialDb.Repositories;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.DbApiTest.Repositories
{
    public interface IMongoUserRepository:IRepository<User>
    {

    }
}
