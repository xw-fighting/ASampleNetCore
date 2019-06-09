using ASample.NetCore.Domain.Exceptions;
using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.RabbitMq.Publisher;
using ASample.NetCore.Services.Customers.Messages.Commands;
using ASample.NetCore.Services.Customers.Messages.Events;
using ASample.NetCore.Services.Customers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Handlers.Customers
{
    public class AddProductToCartHandler : ICommandHandler<AddProductToCartCommand>
    {

        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IBusPublisher _busPublisher;

        public AddProductToCartHandler(ICartsRepository cartsRepository
            , IProductsRepository productsRepository
            , IBusPublisher busPublisher)
        {
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(AddProductToCartCommand command, ICorrelationContext context)
        {
            if(command.Quantity <= 0)
            {
                throw new ASampleException(Codes.InvalidQuantity,
                    $"Invalid quantity: '{command.Quantity}'.");
            }

            var product = await _productsRepository.GetAsync(command.ProductId);
            if (product == null)
            {
                throw new ASampleException(Codes.ProductNotFound,
                    $"Product: '{command.ProductId}' was not found.");
            }

            if (product.Quantity < command.Quantity)
            {
                throw new ASampleException(Codes.NotEnoughProductsInStock,
                    $"Not enough products in stock: '{command.ProductId}'.");
            }

            var cart = await _cartsRepository.GetAsync(command.CustomerId);
            cart.AddProduct(product, command.Quantity);
            await _cartsRepository.UpdateAsync(cart);
            await _busPublisher.PublishAsync(new ProductAddedToCartEvent(command.CustomerId,
                command.ProductId, command.Quantity), context);
        }
    }
}
