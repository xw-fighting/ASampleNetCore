
using ASample.NetCore.Domain.AggregateRoots;
using ASample.NetCore.EntityFramwork.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASample.NetCore.EntityFramwork
{
    public interface IASampleRepository<TEntity> : IASampleBaseRepository<TEntity, Guid> where TEntity : AggregateRoot
    {

    }

    public interface IASampleBaseRepository<TEntity,TKey> where TEntity : AggregateRoot<TKey>
    {
        Task<TEntity> GetAsync(TKey id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
               TQuery query) where TQuery : PagedQueryBase;

        Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate = null);

        Task<PagedResult<TEntity>> QueryPagedAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
             TQuery query) where TQuery : PagedQueryBase;

        Task<PagedResult<TEntity>> QueryPagedAsync(int page, int limit
            , Func<TEntity, dynamic> sortLamda = null
            , Func<IQueryable<TEntity>, IQueryable<TEntity>> whereLamda = null
            , bool isAsc = true);


        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
