using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.MongoDb.Repository;
using ASample.NetCore.Services.Orders.Domain;
using ASample.NetCore.Services.Orders.Domain.Values;
using ASample.NetCore.Services.Orders.Queries;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Orders.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoRepository<Order> _orderRepository;

        public OrdersRepository(IMongoRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> HasPendingOrder(Guid customerId)
        {
            var result = await _orderRepository.ExistsAsync(o => o.CustomerId == customerId
                &&(o.Status == OrderStatus.Created 
                || o.Status == OrderStatus.Approved));
            return result;

        }

        public async Task<Order> GetAsync(Guid id)
        {
            var order = await _orderRepository.GetAsync(id);
            return order;
        }

        public async Task<PagedResult<Order>> QueryPagedAsync(BrowseOrders query)
        {
            var pagedResult = await _orderRepository.QueryPagedAsync(o => o.CustomerId == query.CustomerId, query);
            return pagedResult;
        }

        public async Task AddAsync(Order order)
           => await _orderRepository.AddAsync(order);

        public async Task UpdateAsync(Order order)
            => await _orderRepository.UpdateAsync(order);

        public async Task DeleteAsync(Guid id)
            => await _orderRepository.DeleteAsync(id);
    }
}
