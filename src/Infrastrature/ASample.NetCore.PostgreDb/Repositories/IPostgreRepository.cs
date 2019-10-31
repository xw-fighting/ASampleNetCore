using ASample.NetCore.Domain;
using ASample.NetCore.EntityFramwork;

namespace ASample.NetCore.PostgreDb.Repositories
{
    public interface IPostgreRepository<TEntity> : IASampleRepository<TEntity>
        where TEntity : AggregateRoot
    {

    }
}
