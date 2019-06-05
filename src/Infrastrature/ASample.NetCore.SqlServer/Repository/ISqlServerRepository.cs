using ASample.NetCore.Domain.Models;
using ASample.NetCore.EntityFramwork;

namespace ASample.NetCore.SqlServer.Repository
{
    public interface ISqlServerRepository<TEntity> :IASampleRepository<TEntity> 
        where TEntity:AggregateRoot
    {

    }
}
