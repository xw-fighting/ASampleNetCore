using ASample.NetCore.Domain.AggregateRoots;
using ASample.NetCore.Extension;
using ASample.NetCore.MongoDb.Options;
using ASample.NetCore.MongoDb.Repository;
using Autofac;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ASample.NetCore.MongoDb
{
    public static class Extension
    {
        public static void AddMongo(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<MongoDbOptions>("mongo");

                return options;
            }).SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<MongoDbOptions>();

                return new MongoClient(options.ConnectionString);
            }).SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<MongoDbOptions>();
                var client = context.Resolve<MongoClient>();
                return client.GetDatabase(options.Database);

            }).InstancePerLifetimeScope();

            builder.RegisterType<MongoDbInitializer>()
                .As<IMongoDbInitializer>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MongoDbSeeder>()
                .As<IMongoDbSeeder>()
                .InstancePerLifetimeScope();
        }

        public static void AddMongoRepository<TEntity>(this ContainerBuilder builder, string collectionName = null)
            where TEntity : AggregateRoot
            => builder.Register(ctx => new MongoRepository<TEntity>(ctx.Resolve<IMongoDatabase>(), collectionName))
                .As<IMongoRepository<TEntity>>()
                .InstancePerLifetimeScope();


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


        public static Guid ToCSUUid(this Guid guid)
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.PythonLegacy;
            var luuid = new Guid(guid.ToString());
            var bytes = GuidConverter.ToBytes(luuid, GuidRepresentation.PythonLegacy);
            var csuuid = new Guid(bytes);
            return csuuid;
        }
    }
}
