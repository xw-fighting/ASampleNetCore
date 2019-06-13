using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Orders.Dtos;
using ASample.NetCore.Services.Orders.Queries;
using ASample.NetCore.Services.Orders.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Orders.Handlers
{
    public class BrowseOrdersHandler : IQueryHandler<BrowseOrders, PagedResult<OrderDto>>
    {
        private readonly IOrdersRepository _ordersRepository;

        public BrowseOrdersHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<PagedResult<OrderDto>> HandleAsync(BrowseOrders query)
        {
            var pagedResult = await _ordersRepository.QueryPagedAsync(query);
            if (pagedResult.IsEmpty)
                return null;
            var orders = pagedResult.Items.Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                ItemsCount = o.Items.Count(),
                TotalAmount = o.TotalAmount,
                Currency = o.Currency,
                Status = o.Status.ToString().ToLowerInvariant(),
            });
            return PagedResult<OrderDto>.From(pagedResult, orders);
        }
    }
}
