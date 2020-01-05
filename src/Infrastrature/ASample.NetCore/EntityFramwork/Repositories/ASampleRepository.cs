using ASample.NetCore.Domain;
using ASample.NetCore.EntityFramwork.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASample.NetCore.EntityFramwork
{
    public abstract class ASampleRepository<TEntity>: ASampleBaseRepository<TEntity,Guid>,IASampleRepository<TEntity> where TEntity:class,IAggregateRoot
    {

    }

    public abstract class ASampleBaseRepository<TEntity, TKey> : IASampleBaseRepository<TEntity, TKey> where TEntity : class ,IAggregateRoot<TKey>
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
            var result = GetAll().Where(predicate);
            return result.PaginateAsync();
        }

        public virtual async Task<PagedResult<TEntity>> QueryPagedAsync(int pageNum, int pageSize
            , Func<TEntity, dynamic> sortLamda = null
            , Func<IQueryable<TEntity>,IQueryable<TEntity>> whereLamda = null
            , bool isAsc = true)
        {
            IQueryable<TEntity> result;

            try
            {
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
                    result = queryable.OrderBy(sortLamda).AsQueryable();
                }
                else
                {
                    result = queryable.OrderByDescending(sortLamda).AsQueryable();
                }
                return await result.PaginateAsync(pageNum, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public abstract Task MultipleInsert(List<TEntity> entity);

        public virtual Task AddAsync(TEntity entity)
        {
            entity.CreateTime = DateTime.Now;
            return Task.FromResult(Insert(entity));
        }

        public virtual Task MultiAddAsync(List<TEntity> entitys)
        {
            entitys.ForEach(c =>
            {
                c.CreateTime = DateTime.Now;
            });
            return Task.FromResult(MultipleInsert(entitys));
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
        public abstract Task MultipleUpdate(List<TEntity> entity);

        public virtual Task UpdateAsync(TEntity entity)
        {
            entity.ModifyTime = DateTime.Now;
            return Task.FromResult(Update(entity));
        }

        public virtual Task MultiUpdateAsync(List<TEntity> entitys)
        {
            entitys.ForEach(c =>
            {
                c.ModifyTime = DateTime.Now;
            });
            return Task.FromResult(MultipleUpdate(entitys));
            
        }

        public virtual TEntity Update(TKey id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            return entity;
        }

        public abstract void Delete(TEntity entity);
        public abstract Task PhysicalDelete(TEntity entity);
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

        /// <summary>
        /// 逻辑删除数据
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate,bool isLogicDel = true)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                entity.IsDeleted = true;
                entity.DeleteTime = DateTime.Now;
                if (isLogicDel)
                    Update(entity);
                else
                    PhysicalDelete(entity);
            }
        }

        /// <summary>
        /// 逻辑删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate,bool isLogicDel = true)
        {
            Delete(predicate,isLogicDel);
            return Task.FromResult(0);
        }

        /// <summary>
        /// 构建相等的lambda 表达式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity),"x");

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TKey))
                );
            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        public virtual Task MultiDeleteAsync(List<TEntity> entitys)
        {
            entitys.ForEach(item =>
            {
                item.DeleteTime = DateTime.Now;
                item.IsDeleted = true;
            });
            return Task.FromResult(MultiUpdateAsync(entitys));
        }
    }
}
