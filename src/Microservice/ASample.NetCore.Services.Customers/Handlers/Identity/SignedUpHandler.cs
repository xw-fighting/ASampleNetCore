using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Customers.Domain;
using ASample.NetCore.Services.Customers.Messages.Events;
using ASample.NetCore.Services.Customers.Repositories;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Handlers
{
    public class SignedUpHandler : IEventHandler<SignedUpEvent>
    {
        private readonly ICustomersRepository _customersRepository;

        public SignedUpHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task HandleAsync(SignedUpEvent @event, ICorrelationContext context)
        {
            var customer = new Customer(@event.UserId, @event.Email);
            await _customersRepository.AddAsync(customer);
        }
    }
}
