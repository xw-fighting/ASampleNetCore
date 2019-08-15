using ASample.NetCore.Domain.AggregateRoots;
using ASample.NetCore.EntityFramwork;
using Microsoft.EntityFrameworkCore;

namespace ASample.NetCore.SqlServerDb.Repository
{
    public interface ISqlServerRepository<TEntity> :IASampleRepository<TEntity> 
        where TEntity:AggregateRoot
    {

    }
}
