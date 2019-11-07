using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASample.NetCore.Dispatchers;
using ASample.NetCore.Redis;
using ASample.NetCore.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace ASample.NetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly IASampleRedisCache _cache;


        public RedisController(IDispatcher dispatcher, IASampleRedisCache cache)
        {
            _dispatcher = dispatcher;
            _cache = cache;
        }

        

        [HttpGet]
        public async Task<string> Get()
        {
            //var hash = await _cache.GetHashAsync("userInfo","1");
            var userInfo = _cache.GetString("hashTest");
            //return hash + userInfo;
            return userInfo;
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
            //await _repository.UpdateAsync(user);
        }

        [HttpPost]
        public async Task HashSetAsync()
        {
            //var hash = new List<HashSetDto>
            //{
            //    new HashSetDto("1", "a"),
            //    new HashSetDto("2", "b"),
            //    new HashSetDto("3", "c")
            //};
            //await _cache.SetHashAsync("hashTest", hash);

            //var result = await _dispatcher.QueryAsync(query);
            //return result.ToList();
        }

        [HttpPost("setasync")]
        public async Task SetAsync()
        {
            var hash = new List<HashSetDto>
            {
                new HashSetDto("1", "a"),
                new HashSetDto("2", "b"),
                new HashSetDto("3", "c")
            };
            await _cache.SetStringAsync("hashTest", "123213");

            //var result = await _dispatcher.QueryAsync(query);
            //return result.ToList();
        }

    }
}