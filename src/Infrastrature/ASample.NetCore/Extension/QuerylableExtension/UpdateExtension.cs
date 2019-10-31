using ASample.NetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ASample.NetCore.Extension
{
    public static class UpdateExtension
    {
        public static TEntity UpdateEntity<TEntity,TParam>(this TParam param,TEntity entity) 
            where TEntity : AggregateRoot 
            where TParam : class,new()
        {
            if (param != null)
            {
                var hasValueDic = GetPropertyValue(param);
                foreach (var item in hasValueDic)
                {
                    var value = hasValueDic[item.Key];
                    if (!string.IsNullOrEmpty(value) && item.Key != "Id")
                    {
                        var t = entity.GetType();
                        var properties = t.GetProperties();//获取到泛型所有属性的集合
                        var updateFiled = properties.FirstOrDefault(c => c.Name == item.Key);
                        object itemVlaue = item.Value;
                        if (updateFiled.PropertyType == typeof(Nullable<Guid>) || updateFiled.PropertyType == typeof(Guid))
                        {
                            itemVlaue = Guid.Parse(item.Value);
                        }
                        if (updateFiled.PropertyType == typeof(DateTime) || updateFiled.PropertyType == typeof(Nullable<DateTime>))
                        {
                            itemVlaue = DateTime.Parse(item.Value);
                        }
                        updateFiled.SetValue(entity, itemVlaue); 
                    }
                }
            }
            return entity;
        }

        /// <summary>
        /// 获取对象的属性和值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回属性与值一一对应的字典</returns>
        public static Dictionary<string, string> GetPropertyValue<T>(T obj)
        {
            var propertyValueDic = new Dictionary<string, string>();
            var type = obj.GetType();
            var propertyInfos = type.GetProperties();

            foreach (PropertyInfo item in propertyInfos)
            {
                if (item.Name == "Limit" || item.Name == "Page")
                    continue;
                var value = item.GetValue(obj);
                if (value != null)
                {
                    propertyValueDic.Add(item.Name, value.ToString());
                }
            }
            return propertyValueDic;
        }
        public static Func<TSource, bool> GetExpression<TSource>(string propertyName, string propertyValue)
        {
            try
            {
                var methodName = "Equals";
                var type = typeof(string);
                var parameter = Expression.Parameter(typeof(TSource), "x");
                var source = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
                Expression<Func<TSource, bool>> lambda;
                if (propertyName.Contains("name", StringComparison.CurrentCultureIgnoreCase))
                    methodName = "Contains";
                if (propertyName.Contains("time", StringComparison.CurrentCultureIgnoreCase)
                                || propertyName.Contains("date", StringComparison.CurrentCultureIgnoreCase))
                {
                    var property = typeof(TSource).GetProperty(propertyName);
                    var left = Expression.Property(parameter, property);
                    //等式右边的值
                    var right = Expression.Constant(Convert.ToDateTime(propertyValue).Date);
                    var right2 = Expression.Constant(Convert.ToDateTime(propertyValue).AddDays(1).Date);
                    var express = Expression.AndAlso(Expression.GreaterThanOrEqual(left, right), Expression.LessThan(left, right2));
                    lambda = Expression.Lambda<Func<TSource, bool>>(express, parameter);
                }
                else
                {
                    var body = Expression.Call(source, methodName, Type.EmptyTypes, Expression.Constant(propertyValue, type));
                    lambda = Expression.Lambda<Func<TSource, bool>>(body, parameter);
                }
                return lambda.Compile();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
