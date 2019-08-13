using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.RabbitMq.Publisher;
using ASample.NetCore.Services.Products.Domain;
using ASample.NetCore.Services.Products.Messages.Commands;
using ASample.NetCore.Services.Products.Messages.Events;
using ASample.NetCore.Services.Products.Repositories;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Products.Handlers
{
    public class CreateProductHandler : ICommandHandler<CreateProductCommand>
    {
        private IProductRepository _productRepository;
        private IBusPublisher _busPublisher;

        public CreateProductHandler(IProductRepository productRepository
            , IBusPublisher busPublish)
        {
            _productRepository = productRepository;
            _busPublisher = busPublish;
        }

        public async Task HandleAsync(CreateProductCommand command, ICorrelationContext context)
        {
            if (command.Quantity < 0)
            {
                throw new ASampleException("invalid_product_quantity",
                    "Product quantity cannot be negative.");
            }

            if (await _productRepository.ExistsAsync(command.Name))
            {
                throw new ASampleException("product_already_exists",
                    $"Product: '{command.Name}' already exists.");
            }

            var product = new Product(command.Name,
                command.Description, command.Vendor, command.Price, command.Quantity);
            await _productRepository.AddAsync(product);

            await _busPublisher.PublishAsync(new ProductCreatedEvent(command.Id, command.Name,
               command.Description, command.Vendor, command.Price, command.Quantity), context);
        }
    }
}
