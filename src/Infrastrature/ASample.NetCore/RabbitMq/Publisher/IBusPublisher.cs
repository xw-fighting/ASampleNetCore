using ASample.NetCore.Domain.Message;
using ASample.NetCore.Domain.RabbitMq;
using System.Threading.Tasks;

namespace ASample.NetCore.RabbitMq
{
    public interface IBusPublisher
    {
        Task SendAsync<TCommand>(TCommand command, ICorrelationContext context)
           where TCommand : ICommand;

        Task PublishAsync<TEvent>(TEvent @event, ICorrelationContext context)
            where TEvent : IEvent;
    }
}
