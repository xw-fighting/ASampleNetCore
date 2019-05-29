using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.MongoDb.Model;
using ASample.NetCore.MongoDb.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoDbController : ControllerBase
    {
        private readonly IMongoRepository<User> _repository;

        public MongoDbController(IMongoRepository<User> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task AddAsync()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "test",
                Address = "云南昆明",
                Age = 21,
                CreateTime = DateTime.Now
            };
            await _repository.AddAsync(user);
        }
    }
}