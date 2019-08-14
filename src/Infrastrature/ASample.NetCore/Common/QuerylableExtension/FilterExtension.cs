﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ASample.NetCore.Common
{
    public static class FilterExtension
    {
        public static Func<IQueryable<T>, IQueryable<T>> SearchFilter<T,TParam>(this TParam param) where T:class,new() where TParam:class
        {
            IQueryable<T> queryable(IQueryable<T> q)
            {
                if (param != null)
                {
                    var hasValueDic = GetPropertyValue(param);
                    foreach (var item in hasValueDic)
                    {
                        var value = hasValueDic[item.Key];
                        if (!string.IsNullOrEmpty(value))
                        {
                            Func<T, bool> expression = GetExpression<T, TParam>(item.Key, hasValueDic[item.Key]);
                            q = q.Where(expression).AsQueryable();
                        }
                    }
                }
                return q;
            }
            return queryable;
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

        /// <summary>
        /// 构建 Lambda 表达式
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        public static Func<TSource, bool> GetExpression<TSource,TParam>(string propertyName, string propertyValue)
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
                    var express = Expression.AndAlso(Expression.GreaterThanOrEqual(left, right),Expression.LessThan(left, right2));
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


        public static TValue IfNotNull<T, TValue>(this T objectT, Func<T, TValue> selector)
        {
            return objectT == null ? default(TValue) : selector(objectT);
        }
    }
}