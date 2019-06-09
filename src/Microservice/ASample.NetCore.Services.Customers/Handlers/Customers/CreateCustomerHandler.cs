using ASample.NetCore.Domain.Exceptions;
using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.RabbitMq.Publisher;
using ASample.NetCore.Services.Customers.Domain;
using ASample.NetCore.Services.Customers.Messages.Commands;
using ASample.NetCore.Services.Customers.Messages.Events;
using ASample.NetCore.Services.Customers.Repositories;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Handlers
{
    public class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly ICartsRepository _cartsRepository;
        private readonly IBusPublisher _busPublisher;

        public CreateCustomerHandler(ICustomersRepository customersRepository, IBusPublisher busPublisher)
        {
            _customersRepository = customersRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CreateCustomerCommand command, ICorrelationContext context)
        {
            var customer = await _customersRepository.GetAsync(command.Id);
            if (customer.Completed)
            {
                throw new ASampleException(Codes.CustomerAlreadyCompleted,
                    $"Customer account was already created for user with id: '{command.Id}'.");
            }

            customer.Complete(command.FirstName, command.LastName, command.Address, command.Country);
            await _customersRepository.UpdateAsync(customer);
            var cart = new Cart(command.Id);
            await _cartsRepository.AddAsync(cart);
            await _busPublisher.PublishAsync(new CustomerCreatedEvent(command.Id, customer.Email,
                command.FirstName, command.LastName, command.Address, command.Country), context);
        }
    }
}
