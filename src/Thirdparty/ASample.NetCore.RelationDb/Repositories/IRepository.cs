using ASample.NetCore.Domain;
using ASample.NetCore.EntityFramwork;

namespace ASample.NetCore.RelationDb.Repositories
{
    public interface IRepository<TEntity> : IASampleRepository<TEntity>
        where TEntity : class, IAggregateRoot
    {

    }
}
