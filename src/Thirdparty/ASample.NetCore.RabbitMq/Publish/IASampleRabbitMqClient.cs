using System;
using System.Collections.Generic;
using System.Text;

namespace ASample.NetCore.RabbitMq.Publish
{
    public interface IASampleRabbitMqClient
    {
        void Publish();

        void Subscribe();

        void Send(string message);

        void Receive(Action<string> action);
    }
}
