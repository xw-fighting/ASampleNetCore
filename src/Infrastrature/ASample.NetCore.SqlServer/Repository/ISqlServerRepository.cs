using ASample.NetCore.Domain.AggregateRoots;
using ASample.NetCore.EntityFramwork;

namespace ASample.NetCore.SqlServerDb.Repository
{
    public interface ISqlServerRepository<TEntity> :IASampleRepository<TEntity> 
        where TEntity:AggregateRoot
    {

    }
}
