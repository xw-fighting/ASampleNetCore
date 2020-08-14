using System;
using RabbitMQ.Client;

namespace ASample.NetCore.RabbitMq.Publish
{
    public interface IASampleRabbitMqClient
    {
        void Publish(string message, string exchangeType = ExchangeType.Fanout);

        void Subscribe(Action<string> handler, string exchangeType = ExchangeType.Fanout);

        void Send(string message);

        void Receive(Action<string> action);
    }
}
