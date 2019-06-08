using ASample.NetCore.Domain.Exceptions;
using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.RabbitMq.Publisher;
using ASample.NetCore.Services.Products.Messages.Commands;
using ASample.NetCore.Services.Products.Messages.Events;
using ASample.NetCore.Services.Products.Repositories;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Products.Handlers
{
    public class DeleteProductHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IBusPublisher _busPublisher;

        public DeleteProductHandler(IProductRepository productRepository, IBusPublisher busPublisher)
        {
            _productRepository = productRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(DeleteProductCommand command, ICorrelationContext context)
        {
            var isExist = await _productRepository.ExistsAsync(command.Id);
            if(!isExist)
            {
                throw new ASampleException("product_not_found",
                    $"Product with id: '{command.Id}' was not found.");
            }
            await _productRepository.DeleteAsync(command.Id);
            await _busPublisher.PublishAsync(new ProductDeletedEvent(command.Id), context);
        }
    }
}
