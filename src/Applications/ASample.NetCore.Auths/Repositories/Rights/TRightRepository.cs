using ASample.NetCore.Auths.DbConexts;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.SqlServerDb.Repository;


namespace ASample.NetCore.Auths.Repositories
{
    public class TRightRepository :SqlServerRepository<ASampleIdentityDbContext,TRight>,ITRightRepository
    {
        private readonly IUnitOfWork<ASampleIdentityDbContext> _unitOfWork;
        public TRightRepository(IUnitOfWork<ASampleIdentityDbContext> unitOfWork) :base(unitOfWork)
        {

        }
    }
}
