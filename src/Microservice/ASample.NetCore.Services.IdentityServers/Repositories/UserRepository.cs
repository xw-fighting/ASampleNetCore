using ASample.NetCore.EntityFramwork;
using ASample.NetCore.RelationalDb;
using ASample.NetCore.Services.IdentityServers.Domain;

namespace ASample.NetCore.Services.IdentityServers.Repositories
{
    public class UserRepository : Repository<ASampleSqlServerDbContext, User>, IUserRepository
    {
        public UserRepository(IUnitOfWork<ASampleSqlServerDbContext> unitOfWork) :base(unitOfWork)
        {
            
        }
    }
}
