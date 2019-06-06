using ASample.NetCore.Domain.Exceptions;
using ASample.NetCore.Domain.Message;
using System;

namespace ASample.NetCore.RabbitMq.Subscriber
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
           Func<TCommand, ASampleException, IRejectedEvent> onError = null)
           where TCommand : ICommand;

        IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
            Func<TEvent, ASampleException, IRejectedEvent> onError = null)
            where TEvent : IEvent;
    }
}
