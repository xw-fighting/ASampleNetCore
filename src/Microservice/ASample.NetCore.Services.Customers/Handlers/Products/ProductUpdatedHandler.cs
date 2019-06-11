using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Customers.Domain;
using ASample.NetCore.Services.Customers.Messages.Events.Products;
using ASample.NetCore.Services.Customers.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Handlers.Products
{
    public class ProductUpdatedHandler : IEventHandler<ProductUpdatedEvent>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICartsRepository _cartsRepository;

        public ProductUpdatedHandler(IProductsRepository productsRepository, ICartsRepository cartsRepository)
        {
            _productsRepository = productsRepository;
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(ProductUpdatedEvent @event, ICorrelationContext context)
        {
            var product = new Product(@event.Id, @event.Name, @event.Price, @event.Quantity);
            await _productsRepository.UpdateAsync(product);

            var carts = await _cartsRepository.GetAllWithProduct(product.Id)
                .ContinueWith(t => t.Result.ToList());

            foreach (var cart in carts)
            {
                cart.UpdateProduct(product);
            }

            await _cartsRepository.UpdateManyAsync(carts);
        }
    }
}
