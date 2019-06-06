using ASample.NetCore.Domain.Models;
using ASample.NetCore.EntityFramwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASample.NetCore.MySql.Repositories
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
        public override void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public override TEntity Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public override TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
