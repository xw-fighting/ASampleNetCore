using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ASample.NetCore.EntityFramwork
{
    public static class Extension
    {
        public static IServiceCollection AddUnitOfWork<TDbContext>(this IServiceCollection services)
            where TDbContext:DbContext
        {
            services.AddScoped<IUnitOfWork<TDbContext>, UnitOfWork<TDbContext>>();
            return services;
        }
    }
}
