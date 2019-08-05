using ASample.NetCore.Domain.AggregateRoots;
using ASample.NetCore.EntityFramwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ASample.NetCore.PostgreDb.Repositories
{
    public class PostgreRepository<TDbContext, TEntity> : ASampleRepository<TEntity>, IPostgreRepository<TEntity>
        where TEntity : AggregateRoot
        where TDbContext : DbContext
    {
        private TDbContext _db;
        private DbSet<TEntity> _dbSet;
        public PostgreRepository(TDbContext dbContext)
        {
            _db = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        public override void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
            //_db.SaveChanges();
        }

        public override void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            //_db.SaveChanges();
        }

        public override IQueryable<TEntity> GetAll()
        {
            var result = _dbSet.AsQueryable();
            return result;
        }

        public override TEntity Insert(TEntity entity)
        {
            var result = _dbSet.Add(entity);
            //_db.SaveChanges();
            return result.Entity;
        }

        public override TEntity Update(TEntity entity)
        {
            var result = _dbSet.Update(entity);
            //_db.SaveChanges();
            return result.Entity;
        }
    }
}
