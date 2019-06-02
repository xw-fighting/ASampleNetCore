using ASample.NetCore.Extension;
using ASample.NetCore.Redis.Options;
using Autofac;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace ASample.NetCore.Redis
{
    public static class Extensions
    {
        private static readonly string SectionName = "redis";

        private static IDistributedCache _cache;

        public static IServiceCollection AddRedis(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.Configure<RedisOptions>(configuration.GetSection(SectionName));
            var options = configuration.GetOptions<RedisOptions>(SectionName);
            
            //services.AddDistributedRedisCache(o =>
            //{
            //    o.Configuration = options.ConnectionString;
            //    o.InstanceName = options.Instance;
            //});


            return services;

        }

        public static IServiceCollection AddRedis(this ContainerBuilder builder ,IServiceCollection services)
        {
            IOptions<RedisCacheOptions> redisCacheOption = null;
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<RedisOptions>(SectionName);

                return options;
            }).SingleInstance();

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<RedisOptions>(SectionName);
                redisCacheOption = new RedisCacheOptions()
                {
                    Configuration = options.ConnectionString,
                    InstanceName = options.Instance
                };
                return redisCacheOption;
            }).SingleInstance();

            //builder.Register(ctx => new ASampleRedisCache(ctx.Resolve<IDatabase>(), redisCacheOption))
            //  .As<IASampleRedisCache>()
            //  .SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<RedisOptions>();
                var redis = ConnectionMultiplexer.Connect(options.ConnectionString);
                return redis.GetDatabase();

            }).SingleInstance();

            builder.RegisterType<ASampleRedisCache>()
               .As<IASampleRedisCache>()
               .SingleInstance();

            return services;
        }


        public static async Task SetAsync<T>(this IDistributedCache redisCache,string key,T TEntity) where T :class
        {
            var jsonStr = JsonConvert.SerializeObject(TEntity);
            var bytes = System.Text.Encoding.Default.GetBytes(jsonStr);
            await redisCache.SetAsync(key, bytes);
        }
    }
}
