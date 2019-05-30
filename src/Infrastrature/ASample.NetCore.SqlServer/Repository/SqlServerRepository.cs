using ASample.NetCore.Domain.Models;
using ASample.NetCore.EntityFramwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASample.NetCore.SqlServer.Repository
{
    public class SqlServerRepository<TEntity> : ASampleRepository<TEntity> where TEntity : AggregateRoot,ISqlServerRepository<TEntity>
    {

        //protected DbContext _dbContext;

        //public SqlServerRepository(DbContext dbContext, string collectionName = null)
        //{
        //    if (!string.IsNullOrEmpty(collectionName))
        //        Collection = database.GetCollection<TEntity>(collectionName);
        //    else
        //        Collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        //}
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
