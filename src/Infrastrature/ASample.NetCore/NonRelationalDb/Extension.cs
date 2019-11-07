using ASample.NetCore.Extension;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ASample.NetCore.NonRelationalDb
{
    public static class Extension
    {
        public static void AddMongoDb<TMongoClient>(this IServiceCollection services, TMongoClient mongoClient)
            where TMongoClient : MongoClient,new()
        {
            services.AddSingleton(mongoClient);
        }

        public static void AddMongo<TMongoClient>(this ContainerBuilder builder)
            where TMongoClient : MongoClient, new()
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<DbOptions>("mongo");
                return options;
            }).SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<DbOptions>();
                //创建带参数的实例
                var mongoClient = (TMongoClient)Activator.CreateInstance(typeof(TMongoClient), options);
                mongoClient.SetProperty(options);
                return mongoClient;
            }).SingleInstance();
        }

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

        //public static TMongoClient SetProperty<TMongoClient>(this TMongoClient client ,DbOptions dbOptions, string propertyName = null)
        //{
        //    var entity = typeof(TMongoClient);
        //    //var assembly = Assembly.GetAssembly(entity);//获取泛型的程序集
        //    var properties = entity.GetProperties();//获取到泛型所有属性的集合

        //    foreach (var  pi in properties)
        //    {
        //        if (pi.Name == propertyName && !string.IsNullOrEmpty(propertyName))
        //        {
        //            pi.SetValue(client, dbOptions, null);//给泛型的属性赋值
        //        }
        //        if (pi.PropertyType.Equals(typeof(DbOptions)))//判断属性的类型是不是DbOptions
        //        {
        //            pi.SetValue(client, dbOptions, null);//给泛型的属性赋值
        //        }
        //    }
        //    return client;
        //}

        //public static DbOptions GetPropertyValue<TMongoClient>(this TMongoClient client, string propertyName = null)
        //{
        //    var entity = typeof(TMongoClient);
        //    //var assembly = Assembly.GetAssembly(entity);//获取泛型的程序集
        //    var properties = entity.GetProperties();//获取到泛型所有属性的集合
        //    var value = new DbOptions();
        //    foreach (var pi in properties)
        //    {
        //        if (pi.Name == propertyName && !string.IsNullOrEmpty(propertyName))
        //        {
        //            value = (DbOptions)pi.GetValue(client);//给泛型的属性赋值
        //        }
        //        if (pi.PropertyType.Equals(typeof(DbOptions)))//判断属性的类型是不是DbOptions
        //        {
        //            value = (DbOptions)pi.GetValue(client);//给泛型的属性赋值
        //        }
        //    }
        //    return value;
        //}
    }
}
