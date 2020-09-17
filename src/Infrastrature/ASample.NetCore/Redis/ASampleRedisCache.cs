using System;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASample.NetCore.Extension;

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

        public async Task SetHashAsync(string key, string name, string value)
        {
            await _redisDatabase.HashSetAsync(key, name, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="expiration">单位分钟，表示多少分钟后过期</param>
        /// <returns></returns>
        public async Task<bool> SetStringAsync(string key, string name, int expiration)
        {
            var timespan = new TimeSpan(0, expiration, 0);
            var result = _redisDatabase.StringSet(key, name, timespan);
            return  await Task.FromResult(result);
        }
    }
}
