using ASample.NetCore.Domain.Exceptions;
using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.RabbitMq.Publisher;
using ASample.NetCore.Services.Orders.Messages.Commands;
using ASample.NetCore.Services.Orders.Messages.Events;
using ASample.NetCore.Services.Orders.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Orders.Handlers.Orders
{
    public class RevokeOrderHandler : ICommandHandler<RevokeOrderCommand>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IBusPublisher _busPublisher;

        public RevokeOrderHandler(IOrdersRepository ordersRepository, IBusPublisher busPublisher)
        {
            _ordersRepository = ordersRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(RevokeOrderCommand command, ICorrelationContext context)
        {
            var order = await _ordersRepository.GetAsync(command.Id);
            if(order == null || order.CustomerId != command.CustomerId)
            {
                throw new ASampleException("order_not_found", $"Order with id: '{command.Id}' " +
                                                            $"was not found for customer with id: '{command.CustomerId}'.");
            }
            order.Revoke();
            await _ordersRepository.UpdateAsync(order);
            await _busPublisher.PublishAsync(new OrderRevokedEvent(command.Id, command.CustomerId), context);
        }
    }
}
