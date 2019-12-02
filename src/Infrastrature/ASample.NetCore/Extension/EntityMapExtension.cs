using ASample.NetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ASample.NetCore.Extension
{
    public static class EntityMapExtension
    {
        public static TResult EntityMap<TSource,TResult>(this TSource tsource)
            where TResult : class, new()
        {
            var source = tsource.GetType();
            var result = typeof(TResult);
            var entity = new TResult();
            var sourcePros = source.GetProperties();
            var resultPros = result.GetProperties();
            foreach (var sourcePro in sourcePros)
            {
                var resultPro = resultPros.FirstOrDefault(c => c.Name == sourcePro.Name);
                if (resultPro != null)
                {
                    try
                    {
                        var value = sourcePro.GetValue(tsource, null);
                        if (resultPro.PropertyType == typeof(DateTime) || resultPro.PropertyType == typeof(Nullable<DateTime>))
                        {
                            value = Convert.ToDateTime(value.ToString());
                        }
                        //if ((sourcePro.PropertyType == typeof(DateTime) || sourcePro.PropertyType == typeof(Nullable<DateTime>)))
                        //{
                        //    value = value.ToString();
                        //}
                        resultPro.SetValue(entity, value, null);
                    }
                    catch
                    { }
                }
            }
            return entity;
        }

        public static TResult EntityMap<TResult, TSource>(this TSource tsource, TResult tresult)
            where TResult : IAggregateRoot
            where TSource : class, new()
        {
            if (tsource != null)
            {
                var hasValueDic = GetPropertyValue(tsource);
                foreach (var item in hasValueDic)
                {
                    var value = hasValueDic[item.Key];
                    if (!string.IsNullOrEmpty(value) && item.Key != "Id")
                    {
                        var t = tresult.GetType();
                        var properties = t.GetProperties();//获取到泛型所有属性的集合
                        var updateFiled = properties.FirstOrDefault(c => c.Name == item.Key);
                        if (updateFiled == null)
                            continue;
                        object itemValue = item.Value;
                        if (updateFiled.PropertyType == typeof(Nullable<Guid>) || updateFiled.PropertyType == typeof(Guid))
                        {
                            itemValue = Guid.Parse(item.Value);
                        }
                        if (updateFiled.PropertyType == typeof(DateTime) || updateFiled.PropertyType == typeof(Nullable<DateTime>))
                        {
                            itemValue = DateTime.Parse(item.Value);
                        }

                        if (updateFiled.PropertyType == typeof(Int32) || updateFiled.PropertyType == typeof(Nullable<Int32>))
                        {
                            itemValue = Convert.ToInt32(item.Value);
                        }

                        if (updateFiled.PropertyType == typeof(Boolean) || updateFiled.PropertyType == typeof(Nullable<Boolean>))
                        {
                            itemValue = Convert.ToBoolean(item.Value);
                        }
                        //Convert.ChangeType does not handle conversion to nullable types
                        //if the property type is nullable, we need to get the underlying type of the property
                        //var targetType = IsNullableType(updateFiled.PropertyType) ? Nullable.GetUnderlyingType(updateFiled.PropertyType) : updateFiled.PropertyType;
                        if (updateFiled.PropertyType.IsEnum)
                        {
                            var enumValue = Enum.Parse(updateFiled.PropertyType, item.Value);
                            itemValue =  enumValue;
                        }
                        //if (updateFiled.PropertyType == typeof(Enum))
                        //{
                        //    itemVlaue = item.Value;
                        //}
                        updateFiled.SetValue(tresult, itemValue);
                    }
                }
            }
            return tresult;
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
