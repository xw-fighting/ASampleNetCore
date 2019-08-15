using ASample.NetCore.Domain.AggregateRoots;
using ASample.NetCore.EntityFramwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASample.NetCore.RelationalDb.Repositories
{
    public class Repository<TDbContext, TEntity> : ASampleRepository<TEntity>, IRepository<TEntity>
        where TEntity : AggregateRoot
        where TDbContext : DbContext
    {
        private DbSet<TEntity> _dbSet;
        public Repository(IUnitOfWork<TDbContext> unitOfWork)
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
