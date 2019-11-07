using ASample.NetCore.Auths.DbConexts;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.RelationalDb;

namespace ASample.NetCore.Auths.Repositories
{
    public class TRightRepository :Repository<ASampleIdentityDbContext,TRight>,ITRightRepository
    {
        private readonly IUnitOfWork<ASampleIdentityDbContext> _unitOfWork;
        public TRightRepository(IUnitOfWork<ASampleIdentityDbContext> unitOfWork) :base(unitOfWork)
        {

        }
    }
}
