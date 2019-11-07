using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.RabbitMq;
using ASample.NetCore.Services.Orders.Messages.Commands;
using ASample.NetCore.Services.Orders.Messages.Events;
using ASample.NetCore.Services.Orders.Repositories;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Orders.Handlers.Orders
{
    public class CompleteOrderHandler : ICommandHandler<CompleteOrderCommand>
    {
        private IOrdersRepository _ordersRepository;
        private IBusPublisher _busPublisher;

        public CompleteOrderHandler(IOrdersRepository ordersRepository, IBusPublisher busPublisher)
        {
            _ordersRepository = ordersRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CompleteOrderCommand command, ICorrelationContext context)
        {
            var order = await _ordersRepository.GetAsync(command.Id);
            if (order == null || order.CustomerId != command.CustomerId)
            {
                throw new ASampleException("order_not_found",
                    $"Order with id: '{command.Id}' was not found for customer with id: '{command.CustomerId}'.");
            }
            order.Complete();
            await _ordersRepository.UpdateAsync(order);

            await _busPublisher.PublishAsync(new OrderCompletedEvent(command.Id, command.CustomerId), context);
        }
    }
}
