using ASample.NetCore.Domain.Models;
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
        private TDbContext _db;
        private DbSet<TEntity> _dbSet;
        public MySqlRepository(TDbContext dbContext)
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
