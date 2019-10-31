using ASample.NetCore.Domain;
using ASample.NetCore.EntityFramwork;

namespace ASample.NetCore.NonInertialDb.Repositories
{
    public interface IRepository<TEntity>:IASampleRepository<TEntity> where TEntity : AggregateRoot
    {

    }
}
