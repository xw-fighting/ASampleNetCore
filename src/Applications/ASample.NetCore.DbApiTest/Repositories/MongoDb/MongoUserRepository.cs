using ASample.NetCore.DbApiTest.Domain;
using ASample.NetCore.NonInertialDb.Repositories;

namespace ASample.NetCore.DbApiTest.Repositories
{

    public class MongoUserRepository : Repository<ASampleMongoDbContext,User>,IMongoUserRepository
    {

        public MongoUserRepository(ASampleMongoDbContext dbContext):base(dbContext)
        {
            
        }
    }
}
