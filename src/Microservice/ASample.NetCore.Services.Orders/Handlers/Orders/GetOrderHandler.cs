using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Orders.Dtos;
using ASample.NetCore.Services.Orders.Queries;
using ASample.NetCore.Services.Orders.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Orders.Handlers.Orders
{
    public class GetOrderHandler : IQueryHandler<GetOrder, OrderDetailDto>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICustomersRepository _customersRepository;

        public GetOrderHandler(IOrdersRepository ordersRepository, ICustomersRepository customersRepository)
        {
            _ordersRepository = ordersRepository;
            _customersRepository = customersRepository;
        }

        public async Task<OrderDetailDto> HandleAsync(GetOrder query)
        {
            var order = await _ordersRepository.GetAsync(query.OrderId);
            if (order == null || query.CustomerId.HasValue && query.CustomerId != order.CustomerId)
            {
                return null;
            }

            var customer = await _customersRepository.GetAsync(order.CustomerId);

            var result = new OrderDetailDto
            {
                Id = order.Id,
                ItemsCount = order.Items.Count(),
                TotalAmount = order.TotalAmount,
                Currency = order.Currency,
                Status = order.Status.ToString().ToLowerInvariant(),
                CustomerId = customer.Id,

                Customer = new CustomerDto
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    Country = customer.Country
                },

                Items = order.Items.Select(o => new OrderItemDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Quantity = o.Quantity,
                    TotalPrice = o.TotalPrice,
                    UnitPrice = o.UnitPrice
                })
            };

            return result;
        }
    }
}
