using ASample.NetCore.Domain.Exceptions;
using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.RabbitMq.Publisher;
using ASample.NetCore.Services.Orders.Domain;
using ASample.NetCore.Services.Orders.Messages.Commands;
using ASample.NetCore.Services.Orders.Messages.Events;
using ASample.NetCore.Services.Orders.Repositories;
using ASample.NetCore.Services.Orders.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Orders.Handlers
{
    public class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly IOrdersRepository _orderRepository;
        private readonly ICustomersService _customersService;
        private readonly IBusPublisher _busPublisher;

        public CreateOrderHandler(IOrdersRepository orderRepository
            , ICustomersService customersService
            , IBusPublisher busPublisher)
        {
            _orderRepository = orderRepository;
            _customersService = customersService;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CreateOrderCommand command, ICorrelationContext context)
        {
            var result = await _orderRepository.HasPendingOrder(command.CustomerId);
            if (result)
            {
                throw new ASampleException("customer_has_pending_order",
                    $"Customer with id: '{command.CustomerId}' has already a pending order.");
            }

            var cart = await _customersService.GetCartAsync(command.CustomerId);
            var items = cart.Items.Select(i => new OrderItem(i.ProductId,
                i.ProductName, i.Quantity, i.UnitPrice)).ToList();
            var order = new Order(command.Id, command.CustomerId, items, "USD");
            await _orderRepository.AddAsync(order);
            await _busPublisher.PublishAsync(new OrderCreatedEvent(command.Id, command.CustomerId,
                items.ToDictionary(i => i.Id, i => i.Quantity)), context);
        }
    }
}
