using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.RabbitMq;
using ASample.NetCore.Services.Products.Messages.Commands;
using ASample.NetCore.Services.Products.Messages.Events;
using ASample.NetCore.Services.Products.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Products.Handlers
{
    public class ReleaseProductHandler : ICommandHandler<ReleaseProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<ReleaseProductHandler> _logger;

        public ReleaseProductHandler(IProductRepository productRepository
            , IBusPublisher busPublisher
            , ILogger<ReleaseProductHandler> logger)
        {
            _productRepository = productRepository;
            _busPublisher = busPublisher;
            _logger = logger;
        }

        public async Task HandleAsync(ReleaseProductCommand command, ICorrelationContext context)
        {
            foreach ((Guid productId, int quantity) in command.Products)
            {
                _logger.LogInformation($"Releasing a product: '{productId}' ({quantity})");
                var product = await _productRepository.GetAsync(productId);
                if (product == null)
                {
                    _logger.LogInformation($"Product was not found: '{productId}' (can't release).");

                    continue;
                }
                product.SetQuantity(product.Quantity + quantity);
                await _productRepository.UpdateAsync(product);
                _logger.LogInformation($"Released a product: '{productId}' ({quantity})");
            }

            await _busPublisher.PublishAsync(new ProductReleasedEvent(command.OrderId,
            command.Products), context);
        }
    }
}
