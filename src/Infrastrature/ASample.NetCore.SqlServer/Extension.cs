using ASample.NetCore.Domain.Models;
using ASample.NetCore.Extension;
using ASample.NetCore.SqlServer.Options;
using ASample.NetCore.SqlServer.Repository;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASample.NetCore.SqlServer
{
    public static class Extension
    {
        public static void AddSqlServer<TDbContext>(this IServiceCollection services) 
            where TDbContext:DbContext
        {
            services.AddEntityFrameworkSqlServer()
               .AddDbContext<TDbContext>();
        }

        public static void AddSqlServerRepository<TDbContext,TEntity>(this ContainerBuilder builder)
            where TEntity : AggregateRoot
            where TDbContext : DbContext
            => builder.Register(ctx => new SqlServerRepository<TDbContext,TEntity>(ctx.Resolve<TDbContext>()))
                .As<ISqlServerRepository<TEntity>>()
                .InstancePerLifetimeScope();
    }
}
