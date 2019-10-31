using ASample.NetCore.Domain;
using ASample.NetCore.EntityFramwork;
using MongoDB.Driver;
using System;
using System.Linq;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace ASample.NetCore.MongoDb.Repository
{
    public class MongoRepository<TEntity> : ASampleRepository<TEntity> , IMongoRepository<TEntity>
        where TEntity:AggregateRoot
    {
        protected IMongoCollection<TEntity> Collection { get; }

        public MongoRepository(IMongoDatabase database,string collectionName = null)
        {
            if(!string.IsNullOrEmpty(collectionName))
                Collection = database.GetCollection<TEntity>(collectionName);
            else
                Collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

       

        //public async Task<TEntity> GetAsync(Guid id)
        //    => await GetAsync(e => e.Id == id);

        //public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        //    => await Collection.Find(predicate).SingleOrDefaultAsync();

        //public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        //    => await Collection.Find(predicate).ToListAsync();

        //public async Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
        //        TQuery query) where TQuery : PagedQueryBase
        //    => await Collection.AsQueryable().Where(predicate).PaginateAsync(query);

        //public async Task AddAsync(TEntity entity)
        //    => await Collection.InsertOneAsync(entity);

        //public async Task UpdateAsync(TEntity entity)
        //    => await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);

        //public async Task DeleteAsync(Guid id)
        //    => await Collection.DeleteOneAsync(e => e.Id == id);

        //public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        //    => await Collection.Find(predicate).AnyAsync();

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
