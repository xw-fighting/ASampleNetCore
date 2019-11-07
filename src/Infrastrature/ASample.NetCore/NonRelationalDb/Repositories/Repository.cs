using ASample.NetCore.Domain;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.Extension;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace ASample.NetCore.NonRelationalDb
{
    public class Repository<TMongoClient, TEntity> : ASampleRepository<TEntity>, IRepository<TEntity>
        where TMongoClient : MongoClient
        where TEntity :class, IAggregateRoot
    {
        protected IMongoCollection<TEntity> Collection { get; set; }
        //protected DbOptions Options { get; set; }

        public Repository(TMongoClient mongoClient)
        {
            var options = mongoClient.GetPropertyValue<TMongoClient, DbOptions>();
            if (options == null || string.IsNullOrEmpty(options.Database))
                throw new ASampleException("Mongo DB 数据库名为空");
            Collection = mongoClient.GetDatabase(options.Database).GetCollection<TEntity>(typeof(TEntity).Name);
        }
        public override IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        public override TEntity Insert(TEntity entity)
        {
            Collection.InsertOne(entity);
            return entity;
        }

        public override TEntity Update(TEntity entity)
        {
            entity.ModifyTime = BsonDateTime.Create(DateTime.Now).ToLocalTime();
            Collection.ReplaceOne(e => e.Id == entity.Id, entity);
            return entity;
        }

        public override void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Guid id)
        {
            Collection.DeleteOne(e => e.Id == id);
        }
    }
}
