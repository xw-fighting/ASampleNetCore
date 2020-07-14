using System;
using System.Collections.Generic;
using System.Text;
using ASample.NetCore.Extension;
using Microsoft.Extensions.Configuration;
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

        public ASampleRabbitMqClient(IConnection connection,IConfiguration configuration)// 
        {
            this.Connection = connection;
            //this._Options = configuration.GetSection("rabbitma").Bind(new RabbitMqOption());
            Options = configuration.GetOptions<RabbitMqOption>("rabbitmq");
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
                routingKey: "",
                basicProperties: null,
                body: body);
            
        }

        public void Receive(Action<string> action)
        {
            using var channel = Connection.CreateModel();
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
