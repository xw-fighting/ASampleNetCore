using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Services.Products.Domain;
using ASample.NetCore.Services.Products.Queries;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Products.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(Guid id);
        Task<PagedResult<Product>> QueryPagedAsync(QueryProducts query);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
