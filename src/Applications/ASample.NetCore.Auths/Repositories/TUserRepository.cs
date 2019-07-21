using ASample.NetCore.Auths.Domains;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.SqlServerDb.Repository;


namespace ASample.NetCore.Auths.Repositories
{
    public class TUserRepository : BaseRepository<TUser>,ITUserRepository
    {

        public TUserRepository(
            ISqlServerRepository<TUser> sqlServerRepository
            , IUnitOfWork unitOfWork
            ):base(sqlServerRepository, unitOfWork)
        {

        }
    }
}
