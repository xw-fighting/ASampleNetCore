using ASample.NetCore.Domain.RabbitMq;
using ASample.NetCore.Handlers;
using ASample.NetCore.MailKit;
using ASample.NetCore.Services.Notifications.Builders;
using ASample.NetCore.Services.Notifications.Messages.Commands;
using ASample.NetCore.Services.Notifications.Services;
using ASample.NetCore.Services.Notifications.Templates;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Notifications.Handlers
{
    public class OrderCreatedHandler : IEventHandler<OrderCreatedEvent>
    {
        private readonly MailKitOptions _options;
        private readonly ICustomersService _customersService;
        private readonly IMessagesService _messagesService;

        public OrderCreatedHandler(
            MailKitOptions options,
            ICustomersService customersService,
            IMessagesService messagesService)
        {
            _options = options;
            _customersService = customersService;
            _messagesService = messagesService;
        }
        public async Task HandleAsync(OrderCreatedEvent @event, ICorrelationContext context)
        {
            var orderId = @event.Id.ToString("N");
            var customer = await _customersService.GetAsync(@event.CustomerId);
            var message = MessageBuilder
               .Create()
               .WithReceiver(customer.Email)
               .WithSender(_options.Email)
               .WithSubject(MessageTemplates.OrderCreatedSubject, orderId)
               .WithBody(MessageTemplates.OrderCreatedBody, customer.FirstName, customer.LastName, orderId)
               .Build();
            await _messagesService.SendAsync(message);
        }
    }
}
