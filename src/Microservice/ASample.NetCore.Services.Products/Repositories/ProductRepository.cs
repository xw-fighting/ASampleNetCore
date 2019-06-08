using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.MongoDb.Repository;
using ASample.NetCore.Services.Products.Domain;
using ASample.NetCore.Services.Products.Queries;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Products.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private IMongoRepository<Product> _repostory;

        public ProductRepository(IMongoRepository<Product> repostory)
        {
            _repostory = repostory;
        }

        public async Task<Product> GetAsync(Guid id)
        {
            var product = await _repostory.GetAsync(id);
            return product;
        }

        public async Task<PagedResult<Product>> QueryPagedAsync(QueryProducts query)
        {
            var products = await _repostory.QueryPagedAsync(p => p.Price >= query.PriceFrom 
            && p.Price < query.PriceTo,query);
            return products;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var result = await _repostory.ExistsAsync(p =>p.Id == id);
            return result;
;       }

        public async Task<bool> ExistsAsync(string name)
        {
            var result = await _repostory.ExistsAsync(p => p.Name == name.ToLowerInvariant());
            return result;
        }

        public async Task AddAsync(Product product)
        {
            await _repostory.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await _repostory.UpdateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repostory.DeleteAsync(id);
        }
    }
}
