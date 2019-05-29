using ASample.NetCore.Dispatchers;
using ASample.NetCore.Handlers;
using ASample.NetCore.MongoDb.Repository;
using ASample.NetCore.MongoDb.Test.Model;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace ASample.NetCore.MongoDb.Test
{
    [TestClass]
    public class MongoDbTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        public IContainer Container { get; private set; }

        public IServiceProvider InitMongoDb()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            //builder.Populate(services);
            builder.AddDispatchers();
            //builder.AddRabbitMq();
            builder.AddMongo();
            builder.AddMongoRepository<User>("User");
            //builder.RegisterGeneric(typeof(GenericEventHandler<>))
            //    .As(typeof(IEventHandler<>));
            //builder.RegisterGeneric(typeof(GenericCommandHandler<>))
            //    .As(typeof(ICommandHandler<>));
            Container = builder.Build();
            var serviceProvider =  new AutofacServiceProvider(Container);
            return serviceProvider;
        }

        [TestMethod]
        public void AddAsyncTest()
        {
            InitMongoDb();
            var repository = Container.Resolve<IMongoRepository<User>>();
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "test",
                Address = "ÔÆÄÏÀ¥Ã÷",
                Age = 21,
                CreateTime = DateTime.Now
            };
            repository.AddAsync(user).GetAwaiter().GetResult();
        }
    }
}
