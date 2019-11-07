using ASample.NetCore.NonRelationalDb;
using ASample.NetCore.Services.Customers.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IRepository<Product> _repository;

        public ProductsRepository(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Product> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task AddAsync(Product product)
            => await _repository.AddAsync(product);

        public async Task UpdateAsync(Product product)
            => await _repository.UpdateAsync(product);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}
