using ASample.NetCore.Domain.AggregateRoots;
using ASample.NetCore.EntityFramwork.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASample.NetCore.EntityFramwork
{
    public abstract class ASampleRepository<TEntity>: ASampleBaseRepository<TEntity,Guid>,IASampleRepository<TEntity> where TEntity:AggregateRoot
    {

    }

    public abstract class ASampleBaseRepository<TEntity, TKey> : IASampleBaseRepository<TEntity, TKey> where TEntity : AggregateRoot<TKey>
    {
        public abstract IQueryable<TEntity> GetAll();

        public virtual Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return Task.FromResult(GetAll());
            else
                return  Task.FromResult(GetAll().Where(predicate));
        }

        public virtual Task<PagedResult<TEntity>> QueryPagedAsync<TQuery>(Expression<Func<TEntity, bool>> predicate, TQuery query) where TQuery : PagedQueryBase
        {
            return GetAll().Where(predicate).PaginateAsync();
        }

        public virtual async Task<PagedResult<TEntity>> QueryPagedAsync(int page, int limit
            , Func<TEntity, dynamic> sortLamda = null
            , Func<IQueryable<TEntity>,IQueryable<TEntity>> whereLamda = null
            , bool isAsc = true)
        {
            IQueryable<TEntity> result;

            var queryable = whereLamda(GetAll());
            if (queryable.Count() <= 0)
            {
                return new PagedResult<TEntity> { Items = new List<TEntity>() };
            }
            if (sortLamda == null)
            {
                sortLamda = o => o.CreateTime;
            }
            if (isAsc)
            {
                result = queryable.OrderBy(sortLamda).Take(limit).Skip((page - 1) * limit).AsQueryable();
            }
            else
            {
                result = queryable.OrderByDescending(sortLamda).Take(limit).Skip((page - 1) * limit).AsQueryable();
            }
            return await result.PaginateAsync(page, limit);
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
            entity.CreateTime = DateTime.Now;
            return Task.FromResult(Insert(entity));
        }

        public virtual TKey AddAndGetId(TEntity entity)
        {
            entity.CreateTime = DateTime.Now;
            return Insert(entity).Id;
        }

        public virtual Task<TKey> AddAndGetIdAsync(TEntity entity)
        {
            entity.CreateTime = DateTime.Now;
            return Task.FromResult(Insert(entity).Id);
        }

        public abstract TEntity Update(TEntity entity);

        public virtual Task UpdateAsync(TEntity entity)
        {
            entity.ModifyTime = DateTime.Now;
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
            var entity = Get(id);
            entity.DeleteTime = DateTime.Now;
            entity.IsDeleted = true;
            //Delete(id);
            return Task.FromResult(Update(entity));
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            var tEntity = Get(entity.Id);
            tEntity.DeleteTime = DateTime.Now;
            tEntity.IsDeleted = true;
            //Delete(id);
            return Task.FromResult(Update(tEntity));
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                entity.IsDeleted = true;
                entity.DeleteTime = DateTime.Now;
                Update(entity);
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
