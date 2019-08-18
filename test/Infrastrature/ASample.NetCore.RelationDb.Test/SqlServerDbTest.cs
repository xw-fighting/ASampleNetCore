using ASample.NetCore.EntityFramwork;
using ASample.NetCore.RelationalDb.Options;
using ASample.NetCore.RelationalDb.Repositories;
using ASample.NetCore.RelationalDb.Test.Model;
using Autofac;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASample.NetCore.RelationDb.Test
{
    [TestClass]
    public class SqlServerDbTest
    {
        public SqlServerDbTest()
        {
            StartupTest.InitStartup();
        }

        [TestMethod]
        public void GetAsyncTest()
        {
            var repository = StartupTest.Container.Resolve<IRepository<User>>();
            var result = repository.GetAsync(Guid.Parse("4D43EDAB-0F1D-4225-8C0F-7A5C8D5ABC6A")).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(result));
            Assert.IsTrue(!string.IsNullOrEmpty(result.Name));
        }

        [TestMethod]
        public void AddAsyncTest()
        {
            var repository = StartupTest.Container.Resolve<IRepository<User>>();
            var unitofWork = StartupTest.Container.Resolve<IUnitOfWork<ASampleSqlServerDbContext>>();
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "test222",
                Address = "云南昆明",
                Age = 21,
                CreateTime = DateTime.Now
            };
            repository.AddAsync(user).GetAwaiter().GetResult();
            var result = unitofWork.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateAsyncTest()
        {

        }

        [TestMethod]
        public void DeleteAsyncTest()
        {

        }
    }
}
