using ASample.NetCore.Domain.Models;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.EntityFramwork.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASample.NetCore.MongoDb.Repository
{
    public interface IMongoRepository<TEntity> :IASampleRepository<TEntity> where TEntity : AggregateRoot
    {
        //Task<TEntity> GetAsync(Guid id);
        //Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        //Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        //Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
        //       TQuery query) where TQuery : PagedQueryBase;
        //Task AddAsync(TEntity entity);
        //Task UpdateAsync(TEntity entity);
        //Task DeleteAsync(Guid id);
        //Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);


    }
}
