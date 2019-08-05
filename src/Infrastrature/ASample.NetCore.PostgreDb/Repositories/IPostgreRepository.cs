using ASample.NetCore.Domain.AggregateRoots;
using ASample.NetCore.EntityFramwork;

namespace ASample.NetCore.PostgreDb.Repositories
{
    public interface IPostgreRepository<TEntity> : IASampleRepository<TEntity>
        where TEntity : AggregateRoot
    {

    }
}
