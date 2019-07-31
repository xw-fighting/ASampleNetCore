using ASample.NetCore.Auths.Domains;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.SqlServerDb.Repository;


namespace ASample.NetCore.Auths.Repositories
{
    public class TRoleRepository : BaseRepository<TRole>,ITRoleRepository
    {
        public TRoleRepository(
            ISqlServerRepository<TRole> sqlServerRepository
            , IUnitOfWork unitOfWork
            ):base(sqlServerRepository, unitOfWork)
        {

        }
    }
}
