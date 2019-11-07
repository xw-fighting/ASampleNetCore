using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.RabbitMq;
using ASample.NetCore.Services.Customers.Messages.Commands;
using ASample.NetCore.Services.Customers.Messages.Events;
using ASample.NetCore.Services.Customers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Handlers.Customers
{
    public class ClearCartHandler : ICommandHandler<ClearCartCommand>
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IBusPublisher _busPublisher;

        public ClearCartHandler(ICartsRepository cartsRepository, IBusPublisher busPublisher)
        {
            _cartsRepository = cartsRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(ClearCartCommand command, ICorrelationContext context)
        {
            var cart = await _cartsRepository.GetAsync(command.CustomerId);
            cart.Clear();
            await _cartsRepository.UpdateAsync(cart);
            await _busPublisher.PublishAsync(new CartClearedEvent(command.CustomerId), context);
        }
    }
}
