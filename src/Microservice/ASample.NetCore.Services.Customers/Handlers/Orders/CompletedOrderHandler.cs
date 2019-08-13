using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Customers.Messages.Events.Orders;
using ASample.NetCore.Services.Customers.Repositories;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Handlers.Orders
{
    public class CompletedOrderHandler : IEventHandler<OrderCompletedEvent>
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public CompletedOrderHandler(ICartsRepository cartsRepository, IProductsRepository productsRepository)
        {
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(OrderCompletedEvent @event, ICorrelationContext context)
        {
            var cart = await _cartsRepository.GetAsync(@event.CustomerId);
            if (cart == null)
                throw new ASampleException(Codes.NotFoundCartByCustomerId, $"{@event.CustomerId}");
            cart.Clear();
            await _cartsRepository.UpdateAsync(cart);
        }
    }
}
