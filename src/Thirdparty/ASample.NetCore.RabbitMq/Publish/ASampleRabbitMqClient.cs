using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ASample.NetCore.RabbitMq.Publish
{
    public class ASampleRabbitMqClient:IASampleRabbitMqClient
    {
        public IConnection Connection { get; }
        public IOptions<RabbitMqOption> _Options { get; }
        public RabbitMqOption Options { get; }

        public ASampleRabbitMqClient(IConnection connection,IOptions<RabbitMqOption> options)
        {
            this.Connection = connection;
            this._Options = options;
            Options = options.Value;
        }
        public void Publish()
        {
            
        }

        public void Subscribe()
        {

        }


        public void Send(string message)
        {
            using var channel = Connection.CreateModel();

            channel.QueueDeclare(queue: Options.QueueName,
                durable: Options.Durable,
                exclusive: Options.Exclusive,
                autoDelete: Options.AutoDelete,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: Options.Exchange,
                routingKey: Options.RoutingKey,
                basicProperties: Options.BasicProperties,
                body: body);
        }

        public void Receive(Action<string> action)
        {
            var channel = Connection.CreateModel();
            channel.QueueDeclare(Options.QueueName,
                durable: Options.Durable,
                exclusive: Options.Exclusive,
                autoDelete: Options.AutoDelete,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                action(message);
            };
            channel.BasicConsume(queue: Options.QueueName,
                autoAck: true,
                consumer: consumer);
        }
    }
}
