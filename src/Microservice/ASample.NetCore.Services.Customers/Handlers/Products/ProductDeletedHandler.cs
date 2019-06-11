using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Customers.Messages.Events.Products;
using ASample.NetCore.Services.Customers.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Handlers.Products
{
    public class ProductDeletedHandler : IEventHandler<ProductDeletedEvent>
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public ProductDeletedHandler(ICartsRepository cartsRepository,
            IProductsRepository productsRepository)
        {
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }
        public async Task HandleAsync(ProductDeletedEvent @event, ICorrelationContext context)
        {
            await _productsRepository.DeleteAsync(@event.Id);
            var carts = await _cartsRepository.GetAllWithProduct(@event.Id)
                .ContinueWith(t => t.Result.ToList());
            foreach (var cart in carts)
            {
                cart.DeleteProduct(@event.Id);
            }

            await _cartsRepository.UpdateManyAsync(carts);
        }
    }
}
