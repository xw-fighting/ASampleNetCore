using ASample.NetCore.EntityFramwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ASample.NetCore.SqlServerDb
{
    public static class Extension
    {
        public static void AddSqlServer<TDbContext>(this IServiceCollection services) 
            where TDbContext:DbContext
        {
            services.AddEntityFrameworkSqlServer()
               .AddDbContext<TDbContext>();
            services.AddUnitOfWork<TDbContext>();
        }

        //public static void AddSqlServerRepository<TDbContext,TEntity>(this ContainerBuilder builder)
        //    where TEntity : AggregateRoot
        //    where TDbContext : DbContext
        //    => builder.Register(ctx => new SqlServerRepository<TDbContext,TEntity>(ctx.Resolve<TDbContext>()))
        //        .As<ISqlServerRepository<TEntity>>()
        //        .InstancePerLifetimeScope();

    }
}
