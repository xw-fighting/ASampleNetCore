using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Services.Orders.Domain;
using ASample.NetCore.Services.Orders.Queries;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Orders.Repositories
{
    public interface  IOrdersRepository
    {
        Task<bool> HasPendingOrder(Guid customerId);

        Task<Order> GetAsync(Guid id);

        Task<PagedResult<Order>> QueryPagedAsync(QueryOrders query);

        Task AddAsync(Order order);

        Task UpdateAsync(Order order);

        Task DeleteAsync(Guid id);
    }
}
