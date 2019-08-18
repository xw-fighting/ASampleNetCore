using ASample.NetCore.RelationalDb.Repositories;
using ASample.NetCore.RelationalDb.Test.Model;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Startup.InitStartup();
        }
        [TestMethod]
        public void GetAsyncTest()
        {
            var repository = Startup.Container.Resolve<IRepository<User>>();
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
        public void AddAsyncTest()
        {

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
