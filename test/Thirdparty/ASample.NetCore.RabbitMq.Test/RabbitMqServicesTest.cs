using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ASample.NetCore.RabbitMq.Publish;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASample.NetCore.RabbitMq.Test
{
    [TestClass]
    public class RabbitMqServicesTest
    {
        public RabbitMqServicesTest()
        {
            StartupTest.InitStartup("rabbitmq");
            var client = StartupTest.Container.Resolve<IASampleRabbitMqClient>();
            client.Subscribe(ReceiveMessage);
        }

        [TestMethod]
        public void Send()
        {
            var client = StartupTest.Container.Resolve<IASampleRabbitMqClient>();
            client.Send("你好");
        }

        [TestMethod]
        public void Receive()
        {
            var client = StartupTest.Container.Resolve<IASampleRabbitMqClient>();
            client.Receive(ReceiveMessage);
        }

        public void ReceiveMessage(string msg)
        {
            Console.WriteLine(msg);
            Trace.WriteLine(msg);
        }

        [TestMethod]
        public void Publish()
        {
            var client = StartupTest.Container.Resolve<IASampleRabbitMqClient>();
            client.Publish("你好");
        }

        [TestMethod]
        public void Subscrible(string msg)
        {
            var client = StartupTest.Container.Resolve<IASampleRabbitMqClient>();
            client.Subscribe(ReceiveMessage);
        }
    }
}
