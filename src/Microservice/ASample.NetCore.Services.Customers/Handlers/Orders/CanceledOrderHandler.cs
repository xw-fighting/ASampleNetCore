using ASample.NetCore.Domain.Exceptions;
using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Customers.Messages.Events.Orders;
using ASample.NetCore.Services.Customers.Repositories;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Handlers.Orders
{
    /// <summary>
    /// 还库存
    /// </summary>
    public class CanceledOrderHandler : IEventHandler<OrderCancelEvent>
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public CanceledOrderHandler(ICartsRepository cartsRepository, IProductsRepository productsRepository)
        {
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(OrderCancelEvent @event, ICorrelationContext context)
        {
            var cart = await _cartsRepository.GetAsync(@event.CustomerId);
            if (cart == null)
                throw new ASampleException(Codes.NotFoundCartByCustomerId, $"{@event.CustomerId}");

            foreach (var cartItem in cart.Items)
            {
                var product =await _productsRepository.GetAsync(cartItem.ProductId);
                if (product == null)
                    continue;
                product.SetQuantity(product.Quantity + cartItem.Quantity);
                await _productsRepository.UpdateAsync(product);
            }
        }
    }
}
