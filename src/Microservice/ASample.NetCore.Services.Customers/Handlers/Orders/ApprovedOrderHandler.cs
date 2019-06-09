
using ASample.NetCore.Domain.Exceptions;
using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Customers.Messages.Events.Orders;
using ASample.NetCore.Services.Customers.Repositories;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Handlers.Orders
{
    public class ApprovedOrderHandler : IEventHandler<OrderApprovedEvent>
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly  IProductsRepository _productsRepository;

        public ApprovedOrderHandler(ICartsRepository cartsRepository, IProductsRepository productsRepository)
        {
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(OrderApprovedEvent @event, ICorrelationContext context)
        {
            var cart = await _cartsRepository.GetAsync(@event.CustomerId);
            if(cart == null )
            {
                throw new ASampleException(Codes.NotFoundCartByCustomerId, $"customerId is {@event.CustomerId}");
            }

            foreach (var cartItem in cart.Items)
            {
                var product =await _productsRepository.GetAsync(cartItem.ProductId);
                if (product == null)
                    continue;
                product.SetQuantity(product.Quantity - cartItem.Quantity);
                await _productsRepository.UpdateAsync(product);
            }
        }
    }
}
