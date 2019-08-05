using ASample.NetCore.Auths.DbConexts;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.SqlServerDb.Repository;


namespace ASample.NetCore.Auths.Repositories
{
    public class TRightRepository : BaseRepository<TRight>,ITRightRepository
    {

        public TRightRepository(
            ISqlServerRepository<TRight> sqlServerRepository
            , IUnitOfWork<ASampleIdentityDbContext> unitOfWork
            ):base(sqlServerRepository, unitOfWork)
        {

        }
    }
}
