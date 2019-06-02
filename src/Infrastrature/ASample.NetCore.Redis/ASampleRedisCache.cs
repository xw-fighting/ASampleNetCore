using ASample.NetCore.Domain.Exceptions;
using ASample.NetCore.Redis.Dto;
using ASample.NetCore.Redis.Options;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.NetCore.Redis
{
    public class ASampleRedisCache:RedisCache,IASampleRedisCache
    {

        private readonly IDatabase _redisDatabase;
        public ASampleRedisCache(IDatabase redisDatabase, IOptions<RedisCacheOptions> options) : base(options)
        {
            _redisDatabase = redisDatabase;
        }
       

        public async Task<string> GetHashAsync(string key, string value)
        {
            var result = await _redisDatabase.HashGetAsync(key, value);
            return result;
        }
        public async Task<List<HashSetDto>> GetHashAllAsync(string key)
        {
            var hashSet = await _redisDatabase.HashGetAllAsync(key);
            var hashDtos = new List<HashSetDto>();
            foreach (var item in hashSet)
            {
                var hashSetDto = new HashSetDto(item.Name, item.Value);
                hashDtos.Add(hashSetDto);
            }
            return hashDtos;
        }

        public async Task SetHashAsync(string key, List<HashSetDto> hashSetDtos)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ASampleException("HashSet key must not be null");
            var haseEntry = new List<HashEntry>();
            foreach (var item in hashSetDtos)
            {
                var hashEntry = new HashEntry(item.Name, item.Value);
                haseEntry.Add(hashEntry);
            }    
            await _redisDatabase.HashSetAsync(key, haseEntry.ToArray());
        }

        public async Task SetHashAsync(string key, string name,string value)
        {
            await _redisDatabase.HashSetAsync(key, name, value);
        }
    }
}
