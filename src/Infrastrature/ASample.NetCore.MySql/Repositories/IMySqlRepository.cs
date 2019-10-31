using ASample.NetCore.Domain;
using ASample.NetCore.EntityFramwork;

namespace ASample.NetCore.MySqlDb.Repositories
{
    public interface  IMySqlRepository<TEntity> : IASampleRepository<TEntity>
        where TEntity : AggregateRoot
    {

    }
}
