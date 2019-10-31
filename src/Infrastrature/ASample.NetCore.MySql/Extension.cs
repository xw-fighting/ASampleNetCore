using ASample.NetCore.Domain;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.MySqlDb.Repositories;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ASample.NetCore.MySqlDb
{
    public static class Extension
    {
        public static void AddMySql<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>();
            services.AddUnitOfWork<TDbContext>();
        }

        //public static void AddMySqlRepository<TDbContext, TEntity>(this ContainerBuilder builder)
        //    where TEntity : AggregateRoot
        //    where TDbContext : DbContext
        //    => builder.Register(ctx => new MySqlRepository<TDbContext, TEntity>(ctx.Resolve<TDbContext>()))
        //        .As<IMySqlRepository<TEntity>>()
        //        .InstancePerLifetimeScope();


        public static T Bind<T>(this T model, Expression<Func<T, object>> expression, object value)
            => model.Bind<T, object>(expression, value);

        public static T BindId<T>(this T model, Expression<Func<T, Guid>> expression)
            => model.Bind<T, Guid>(expression, Guid.NewGuid());

        private static TModel Bind<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression,
            object value)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                memberExpression = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            var propertyName = memberExpression.Member.Name.ToLowerInvariant();
            var modelType = model.GetType();
            var field = modelType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .SingleOrDefault(x => x.Name.ToLowerInvariant().StartsWith($"<{propertyName}>"));
            if (field == null)
            {
                return model;
            }

            field.SetValue(model, value);

            return model;
        }
    }
}
