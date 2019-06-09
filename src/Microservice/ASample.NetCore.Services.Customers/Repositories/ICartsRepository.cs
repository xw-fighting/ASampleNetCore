using ASample.NetCore.Services.Customers.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Repositories
{
    public interface ICartsRepository
    {
        Task<Cart> GetAsync(Guid id);
        Task<IEnumerable<Cart>> GetAllWithProduct(Guid productId);
        Task AddAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task UpdateManyAsync(IEnumerable<Cart> carts);
    }
}
