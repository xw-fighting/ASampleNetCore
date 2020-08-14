using System;
using System.Collections.Generic;
using System.Linq;
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
        
        /// <summary>
        /// RabbitMq 发布
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exchangeType"></param>
        public void Publish(string message, string exchangeType = ExchangeType.Fanout)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            using var channel = Connection.CreateModel();

            channel.ExchangeDeclare(exchange: Options.Exchange, type: exchangeType);

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: Options.Exchange,
                routingKey: Options.RoutingKey,
                basicProperties: null,
                body: body);
        }

        /// <summary>
        /// RabbitMq 订阅
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="exchangeType"></param>
        public void Subscribe(Action<string> handler, string exchangeType = ExchangeType.Fanout)
        {
            var channel = Connection.CreateModel();
            channel.ExchangeDeclare(exchange: Options.Exchange, type: exchangeType);

            var queueName = channel.QueueDeclare(Options.QueueName).QueueName;
            channel.QueueBind(queue: queueName,
                exchange: Options.Exchange,
                routingKey: Options.RoutingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body.ToArray());
                handler(message);
            };
            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="message"></param>
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

        /// <summary>
        /// 监听队列 接收消息
        /// </summary>
        /// <param name="action"></param>
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
