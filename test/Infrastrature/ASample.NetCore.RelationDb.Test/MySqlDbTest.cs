using ASample.NetCore.EntityFramwork;
using ASample.NetCore.RelationalDb.Repositories;
using ASample.NetCore.RelationalDb.Test.Model;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.RelationDb.Test
{
    [TestClass]
    public class MySqlDbTest
    {
        public MySqlDbTest()
        {
            StartupTest.InitStartup("mysql");
        }

        /// <summary>
        /// 测试数据 【4D43EDAB-0F1D-4225-8C0F-7A5C8D5ABC6A】company 【】
        /// </summary>
        [TestMethod]
        public void GetAsyncTest()
        {
            var repository = StartupTest.Container.Resolve<IRepository<User>>();
            var result = repository.GetAsync(Guid.Parse("BE94112D-BF14-4E6B-85CA-E23803AE35D0")).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(result));
            Assert.IsTrue(!string.IsNullOrEmpty(result.Name));
        }

        [TestMethod]
        public void AddAsyncTest()
        {
            var repository = StartupTest.Container.Resolve<IRepository<User>>();
            var unitofWork = StartupTest.Container.Resolve<IUnitOfWork<ASampleMySqlDbContext>>();
            var user = new User
            {
                Id = Guid.Parse("BE94112D-BF14-4E6B-85CA-E23803AE35D0"),
                Name = "test222",
                Address = "123",
                Age = 21,
                //CreateTime = DateTime.Now
            };
            repository.AddAsync(user).GetAwaiter().GetResult();
            try
            {
                var result = unitofWork.SaveChanges();
                Assert.IsTrue(result > 0);
            }
            catch (Exception ex)
            {
                throw;
            }
            
            
        }

        [TestMethod]
        public void UpdateAsyncTest()
        {
            var repository = StartupTest.Container.Resolve<IRepository<User>>();
            var unitofWork = StartupTest.Container.Resolve<IUnitOfWork<ASampleMySqlDbContext>>();
            var user = repository.GetAsync(Guid.Parse("BE94112D-BF14-4E6B-85CA-E23803AE35D0")).GetAwaiter().GetResult();
            user.Address = "testy222";
            repository.UpdateAsync(user).GetAwaiter().GetResult();
            var result = unitofWork.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteAsyncTest()
        {
            var repository = StartupTest.Container.Resolve<IRepository<User>>();
            var unitofWork = StartupTest.Container.Resolve<IUnitOfWork<ASampleMySqlDbContext>>();
            repository.DeleteAsync(Guid.Parse("BE94112D-BF14-4E6B-85CA-E23803AE35D0")).GetAwaiter().GetResult();
            var result = unitofWork.SaveChanges();
            Assert.IsTrue(result > 0);
        }
    }
}
