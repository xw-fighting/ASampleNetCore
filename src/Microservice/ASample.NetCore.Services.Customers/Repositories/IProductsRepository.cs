using ASample.NetCore.Services.Customers.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Repositories
{
    public interface IProductsRepository
    {
        Task<Product> GetAsync(Guid id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
