using ASample.NetCore.Domain.Models;
using ASample.NetCore.EntityFramwork.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASample.NetCore.EntityFramwork
{
    public abstract class ASampleRepository<TEntity>: ASampleBaseRepository<TEntity,Guid>,IASampleRepository<TEntity> where TEntity:AggregateRoot
    {

    }

    public abstract class ASampleBaseRepository<TEntity, TKey> : IASampleBaseRepository<TEntity, TKey> where TEntity : AggregateRoot<TKey>
    {
        public abstract IQueryable<TEntity> GetAll();

        public virtual Task<IQueryable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAll());
        }

        public virtual Task<PagedResult<TEntity>> SelectPagedAsync<TQuery>(Expression<Func<TEntity, bool>> predicate, TQuery query) where TQuery : PagedQueryBase
        {
            return GetAll().PaginateAsync();
        }

        public virtual Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate, TQuery query) where TQuery : PagedQueryBase
        {
            return GetAll().PaginateAsync();
        }

        public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAll().Where(predicate).Any());
        }

        public virtual Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var result = GetAll().Where(predicate);
            return Task.FromResult(result);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public virtual TEntity Get(TKey id)
        {
            var result = Get(CreateEqualityExpressionForId(id));
            return result;
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAll().FirstOrDefault(predicate));
        }

        public virtual Task<TEntity> GetAsync(TKey id)
        {
            var result = GetAsync(CreateEqualityExpressionForId(id));
            return result;
        }

        public abstract TEntity Insert(TEntity entity);
        public virtual Task AddAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public virtual TKey AddAndGetId(TEntity entity)
        {
            return Insert(entity).Id;
        }

        public virtual Task<TKey> AddAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity).Id);
        }

        public abstract TEntity Update(TEntity entity);

        public virtual Task UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        public virtual TEntity Update(TKey id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            return entity;
        }

        public abstract void Delete(TEntity entity);

        public abstract void Delete(TKey id);

        public virtual Task DeleteAsync(TKey id)
        {
            Delete(id);
            return Task.FromResult(0);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
            return Task.FromResult(0);
        }

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }


    }
}
