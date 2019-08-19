using ASample.NetCore.Dispatchers;
using ASample.NetCore.Handlers;
using ASample.NetCore.MongoDb.Repository;
using ASample.NetCore.MongoDb.Test.Model;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace ASample.NetCore.MongoDb.Test
{
    [TestClass]
    public class MongoDbTest
    {
        public IContainer Container { get; private set; }

        //[AssemblyInitialize]
        public IServiceProvider InitMongoDb()
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();
            var configuration = config.Build();
            //services.AddSingleton<IConfiguration>(Configuration);
            var builder = new ContainerBuilder();
            builder.RegisterInstance<IConfiguration>(configuration);
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
            var repository = Container.Resolve<IMongoRepository<User>>();
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "test222",
                Address = "云南昆明",
                Age = 21,
                CreateTime = DateTime.Now
            };
            repository.AddAsync(user).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetAsyncTest()
        {
            InitMongoDb();
            var repository = Container.Resolve<IMongoRepository<User>>();
            var user = new User
            {
                Id = Guid.Parse("a15b8718-2a13-4484-a216-77780a0d1daa"),
                //Name = "test222",
                //Address = "云南昆明",
                //Age = 21,
                //CreateTime = DateTime.Now
            };
            var result = repository.GetAsync(c => c.Id == user.Id).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        [TestMethod]
        public void UpdateAsyncTest()
        {
            InitMongoDb();
            var repository = Container.Resolve<IMongoRepository<User>>();
            var user = new User
            {
                Id = Guid.Parse("a15b8718-2a13-4484-a216-77780a0d1daa"),
                Name = "testUpdate",
                //Address = "云南昆明",
                //Age = 21,
                //CreateTime = DateTime.Now
            };
            var user1 = user.BindId<User>(c => c.Id);
            repository.UpdateAsync(user1).GetAwaiter().GetResult();
        }
    }
}
