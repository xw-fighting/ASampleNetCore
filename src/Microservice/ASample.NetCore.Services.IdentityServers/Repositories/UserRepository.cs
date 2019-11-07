using System.Threading.Tasks;
using ASample.NetCore.NonRelationalDb;
using ASample.NetCore.Services.IdentityServers.Domain;

namespace ASample.NetCore.Services.IdentityServers.Repositories
{
    public class UserRepository : Repository<ASampleMongoDbContext,User>, IUserRepository
    {
        private readonly ASampleMongoDbContext _aSampleMongoDbContext;

        public UserRepository(ASampleMongoDbContext aSampleMongoDbContext):base(aSampleMongoDbContext)
        {
            _aSampleMongoDbContext = aSampleMongoDbContext;
        }
    }
}
