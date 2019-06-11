using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Customers.Domain;
using ASample.NetCore.Services.Customers.Messages.Events.Products;
using ASample.NetCore.Services.Customers.Repositories;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Handlers.Products
{
    public class ProductCreatedHandler : IEventHandler<ProductCreatedEvent>
    {
        private readonly IProductsRepository _productsRepository;

        public ProductCreatedHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(ProductCreatedEvent @event, ICorrelationContext context)
        {
            var product = new Product(@event.Id, @event.Name, @event.Price, @event.Quantity);
            await _productsRepository.AddAsync(product);
        }
    }
}
