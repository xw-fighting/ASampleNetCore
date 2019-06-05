using ASample.NetCore.Domain.Models;
using ASample.NetCore.EntityFramwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ASample.NetCore.SqlServer.Repository
{
    public class SqlServerRepository<TDbContext,TEntity> : ASampleRepository<TEntity> ,ISqlServerRepository<TEntity>
        where TDbContext : DbContext,IEFDbContext
        where TEntity : AggregateRoot

    {
        protected IEFDbContext _dbContext;
        protected DbSet<TEntity> _dbSet; 
        public SqlServerRepository(TDbContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
        }
        public override void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public override void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
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
