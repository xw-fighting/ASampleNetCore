using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.RabbitMq;
using ASample.NetCore.Services.Products.Messages.Commands;
using ASample.NetCore.Services.Products.Messages.Events;
using ASample.NetCore.Services.Products.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Products.Handlers
{
    /// <summary>
    /// 预定商品
    /// </summary>
    public class ReserveProductHandler : ICommandHandler<ReserveProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<ReserveProductHandler> _logger;

        public ReserveProductHandler(IProductRepository productRepository
            , IBusPublisher busPublisher
            , ILogger<ReserveProductHandler> logger)
        {
            _productRepository = productRepository;
            _busPublisher = busPublisher;
            _logger = logger;
        }

        public async Task HandleAsync(ReserveProductCommand command, ICorrelationContext context)
        {
            foreach ((Guid productId,int quantity) in command.Products)
            {
                var product =await _productRepository.GetAsync(productId);
                if(product == null)
                {
                    _logger.LogInformation($"Product was not found: '{productId}' (can't reserve).");
                    continue;
                }

                product.SetQuantity(product.Quantity - quantity);
                await _productRepository.UpdateAsync(product);
            }
            await _busPublisher.PublishAsync(new ProductReservedEvent(command.OrderId,
                command.Products), context);
        }
    }
}
