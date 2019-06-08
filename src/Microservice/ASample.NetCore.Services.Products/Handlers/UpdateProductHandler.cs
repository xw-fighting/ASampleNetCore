using ASample.NetCore.Domain.Exceptions;
using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.RabbitMq.Publisher;
using ASample.NetCore.Services.Products.Messages.Commands;
using ASample.NetCore.Services.Products.Messages.Events;
using ASample.NetCore.Services.Products.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Products.Handlers
{
    public class UpdateProductHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<UpdateProductHandler> _logger;

        public UpdateProductHandler(IProductRepository productRepository
            , IBusPublisher busPublisher
            , ILogger<UpdateProductHandler> logger)
        {
            _productRepository = productRepository;
            _busPublisher = busPublisher;
            _logger = logger;
        }

        public async Task HandleAsync(UpdateProductCommand command, ICorrelationContext context)
        {
            var product = await _productRepository.GetAsync(command.Id);
            if(product == null)
            {
                throw new ASampleException("product_not_found",
                    $"Product with id: '{command.Id}' was not found.");
            }
            product.SetName(command.Name);
            product.SetDescription(command.Description);
            product.SetVendor(command.Vendor);
            product.SetPrice(command.Price);
            product.SetQuantity(command.Quantity);
            await _productRepository.UpdateAsync(product);

            await _busPublisher.PublishAsync(
                new ProductUpdatedEvent(
                command.Id,command.Name,command.Description
                ,command.Vendor, command.Quantity, command.Price), context);
        }
    }
}
