using ASample.NetCore.Domain;
using ASample.NetCore.EntityFramwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ASample.NetCore.MySqlDb.Repositories
{
    public class MySqlRepository<TDbContext, TEntity> : ASampleRepository<TEntity>, IMySqlRepository<TEntity>
        where TEntity:AggregateRoot
        where TDbContext:DbContext
    {
        private DbSet<TEntity> _dbSet;
        public MySqlRepository(IUnitOfWork<TDbContext> unitOfWork)
        {
            _dbSet = unitOfWork.GetDbContext().Set<TEntity>();
        }
        public override void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public override void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public override IQueryable<TEntity> GetAll()
        {
            var result = _dbSet.AsQueryable();
            return result;
        }

        public override TEntity Insert(TEntity entity)
        {
            var result = _dbSet.Add(entity);
            return result.Entity;
        }

        public override TEntity Update(TEntity entity)
        {
            var result = _dbSet.Update(entity);
            return result.Entity;
        }
    }
}
