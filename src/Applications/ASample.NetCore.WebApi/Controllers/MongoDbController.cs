using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Dispatchers;
using ASample.NetCore.MongoDb;
using ASample.NetCore.MongoDb.Options;
using ASample.NetCore.MongoDb.Repository;
using ASample.NetCore.WebApi.Domain;
using ASample.NetCore.WebApi.Messages.Command;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoDbController : ControllerBase
    {
        private readonly IMongoRepository<UserInfo> _repository;
        private readonly IDispatcher _dispatcher;

        public MongoDbController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task AddAsync()
        {
            //var user = new UserInfo
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "test2",
            //    Address = "云南昆明",
            //    Age = 21,
            //};
            //await _repository.AddAsync(user.BindId(c => c.Id));
            var userCommand = new UserInfoCreateCommand
            {
                Id = Guid.NewGuid(),
                Name = "test2",
                Address = "云南昆明",
                Age = 21,
            };
            var command = userCommand.BindId(c => c.Id);
            await _dispatcher.SendAsync(command);
        }

        [HttpGet("{id:guid}")]
        public async Task<UserInfo> Get([FromRoute] UserInfo query)
        {

            var guid = query.Id;//.ToCSUUid();
            //var luuid = new Guid("a2ed42eb-158f-0a41-ba87-a9ac1de2f4a5");
            //var bytes = GuidConverter.ToBytes(luuid, GuidRepresentation.PythonLegacy);
            //var csuuid = new Guid(bytes);
            var discount = await _repository.GetAsync(c => c.Id == guid);

            if (discount is null)
            {
                return null;
            }

            return discount;
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody]UserInfo param)
        {
            var user = new UserInfo
            {
                Id = param.Id,
                Name = "test22",
                Address = "云南昆明市",
                Age = 22,
                CreateTime = DateTime.Now
            };
            await _repository.UpdateAsync(user);
        }

        [HttpGet]
        public async Task<List<UserInfo>> SelectAsync()
        {
            var result = await _repository.SelectAsync(c => true);
            return result.ToList();
        }

    }
}