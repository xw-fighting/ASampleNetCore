using ASample.NetCore.EntityFramwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ASample.NetCore.RelationalDb
{
    public static class Extension
    {
        public static void AddRelationalDb<TDbContext>(this IServiceCollection services)
           where TDbContext : DbContext
        {
            //services.Configure<DbOptions>(configuration);
            services.AddDbContext<TDbContext>();
            services.AddUnitOfWork<TDbContext>();
        }
    }
}
