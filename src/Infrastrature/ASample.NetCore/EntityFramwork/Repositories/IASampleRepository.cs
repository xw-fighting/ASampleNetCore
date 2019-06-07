
using ASample.NetCore.Domain.Models;
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

        Task<IQueryable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate);

        Task<PagedResult<TEntity>> SelectPagedAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
             TQuery query) where TQuery : PagedQueryBase;
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }

    
}
