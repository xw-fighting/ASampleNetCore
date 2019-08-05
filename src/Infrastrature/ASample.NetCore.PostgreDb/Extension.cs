using ASample.NetCore.Domain.AggregateRoots;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.PostgreDb.Repositories;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ASample.NetCore.PostgreDb
{
    public static class Extension
    {
        public static void AddPostgre<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>();
            services.AddUnitOfWork<TDbContext>();
        }

        public static void AddPostgreRepository<TDbContext, TEntity>(this ContainerBuilder builder)
            where TEntity : AggregateRoot
            where TDbContext : DbContext
            => builder.Register(ctx => new PostgreRepository<TDbContext, TEntity>(ctx.Resolve<TDbContext>()))
                .As<IPostgreRepository<TEntity>>()
                .InstancePerLifetimeScope();

    }
}
