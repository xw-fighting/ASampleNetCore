using ASample.NetCore.NonInertialDb.Repositories;
using ASample.NetCore.Services.IdentityServers.Domain;

namespace ASample.NetCore.Services.IdentityServers.Repositories
{
    public class UserRepository : Repository<ASampleMongoDbContext,User>, IUserRepository
    {
        public UserRepository(ASampleMongoDbContext aSampleMongoDbContext):base(aSampleMongoDbContext)
        {

        }
    }
}
