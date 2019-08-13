using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.RabbitMq.Publisher;
using ASample.NetCore.Services.Orders.Messages.Commands;
using ASample.NetCore.Services.Orders.Messages.Events;
using ASample.NetCore.Services.Orders.Repositories;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Orders.Handlers
{
    /// <summary>
    /// 审核订单
    /// </summary>
    public class ApproveOrderHandler : ICommandHandler<ApproveOrderCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IOrdersRepository _ordersRepository;

        public ApproveOrderHandler(IBusPublisher busPublisher, IOrdersRepository ordersRepository)
        {
            _busPublisher = busPublisher;
            _ordersRepository = ordersRepository;
        }

        public async Task HandleAsync(ApproveOrderCommand command, ICorrelationContext context)
        {
            var order = await _ordersRepository.GetAsync(command.Id);
            if (order == null)
            {
                throw new ASampleException("order_not_found",
                    $"Order with id: '{command.Id}' was not found.");
            }

            order.Approve();
            await _ordersRepository.UpdateAsync(order);
            await _busPublisher.PublishAsync(new OrderApprovedEvent(command.Id, order.CustomerId), context);
        }
    }
}
