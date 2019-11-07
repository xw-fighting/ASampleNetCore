using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.RabbitMq;
using ASample.NetCore.Services.Customers.Messages.Commands;
using ASample.NetCore.Services.Customers.Messages.Events;
using ASample.NetCore.Services.Customers.Repositories;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Handlers.Customers
{
    public class DeleteProductFromCartHandler : ICommandHandler<DeleteProductFromCartCommand>
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        private readonly IBusPublisher _busPublisher;

        public DeleteProductFromCartHandler(ICartsRepository cartsRepository, IProductsRepository productsRepository, IBusPublisher busPublisher)
        {
            _cartsRepository = cartsRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(DeleteProductFromCartCommand command, ICorrelationContext context)
        {
            var cart = await _cartsRepository.GetAsync(command.CustomerId);
            cart.DeleteProduct(command.ProductId);
            await _cartsRepository.UpdateAsync(cart);
            await _busPublisher.PublishAsync(new ProductDeletedFromCartEvent(command.CustomerId,
                command.ProductId), context);
        }
    }
}
