using ASample.NetCore.Domain.Message;
using ASample.NetCore.Domain.RabbitMq;
using System.Threading.Tasks;

namespace ASample.NetCore.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, ICorrelationContext context);
    }
}
