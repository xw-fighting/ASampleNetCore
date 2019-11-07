using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASample.NetCore.Redis
{
    public interface IASampleRedisCache : IDistributedCache
    {
        Task SetHashAsync(string key, List<HashSetDto> hashSetDtos);
        Task SetHashAsync(string key, string name, string value);
        Task<string> GetHashAsync(string key, string value);
        Task<List<HashSetDto>> GetHashAllAsync(string key);

    }
}
