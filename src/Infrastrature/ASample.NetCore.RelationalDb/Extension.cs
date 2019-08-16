using ASample.NetCore.EntityFramwork;
using ASample.NetCore.RelationalDb.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASample.NetCore.RelationalDb
{
    public static class Extension
    {
        public static void AddRelationalDb<TDbContext>(this IServiceCollection services, IConfiguration configuration)
           where TDbContext : DbContext
        {
            //services.Configure<DbOptions>(configuration);
            services.AddDbContext<TDbContext>();
            services.AddUnitOfWork<TDbContext>();
        }
    }
}
