using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ASample.NetCore.Common.QuerylableExtension
{
    public static class FilterExtension
    {
        public static Func<IQueryable<T>, IQueryable<T>> SearchFilter<T,TParam>(this TParam param) where T:class,new() where TParam:SearchFilterParam
        {
            Func<IQueryable<T>, IQueryable<T>> queryable = q =>
            {
                if (param != null)
                {
                    var hasValueDic = GetPropertyValue(param);
                    foreach (var item in hasValueDic)
                    {
                        if (!string.IsNullOrEmpty(hasValueDic[item.Key]))
                        {
                            Func<T, bool> expression = null;
                            try
                            {
                                expression = GetFunc<T,TParam>(item.Key, hasValueDic[item.Key]);
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            q = q.Where(expression).AsQueryable();
                        }
                    }
                }
                return q;
            };
            return queryable;
        }

        public static IEnumerable<TSource> Where<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            return WhereImpl(source, predicate);
        }
        private static IEnumerable<TSource> WhereImpl<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static TValue IfNotNull<T, TValue>(this T objectT, Func<T, TValue> selector)
        {
            return objectT == null ? default(TValue) : selector(objectT);
        }

        /// <summary>
        /// 获取对象的属性和值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回属性与值一一对应的字典</returns>
        private static Dictionary<string, string> GetPropertyValue<T>(T obj)
        {
            if (obj != null)
            {
                Dictionary<string, string> propertyValue = new Dictionary<string, string>();
                Type type = obj.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();

                foreach (PropertyInfo item in propertyInfos)
                {
                    var value = item.GetValue(obj);
                    if (value != null)
                    {
                        propertyValue.Add(item.Name, value.ToString());
                    }
                }
                return propertyValue;
            }
            return null;
        }


        private static Expression<Func<TSource, bool>> GetExpression<TSource,TParam>(string propertyName, string propertyValue)
        {
            try
            {
                var methodName = "Equals";
                var type = typeof(string);
                if (propertyName.Contains("name", StringComparison.CurrentCultureIgnoreCase))
                    methodName = "Contains";
                //if(propertyName.Contains("time", StringComparison.CurrentCultureIgnoreCase)
                //    || propertyName.Contains("date", StringComparison.CurrentCultureIgnoreCase))
                //{
                //    type = typeof(DateTime);
                //}
                var parameter = Expression.Parameter(typeof(TSource), "x");
                var source = propertyName.Split('.').Aggregate((Expression)parameter,
                    Expression.Property);
                var body = Expression.Call(source, methodName, Type.EmptyTypes, Expression.Constant(propertyValue, type));
                var lambda = Expression.Lambda<Func<TSource, bool>>(body, parameter);
                return lambda;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // ParameterExpression p = Expression.Parameter(typeof(TSource), "p");
            // var t1 = Expression.CallVirtual(
            //typeof(string).GetMethod("Contains"),
            //Expression.Property(p, typeof(TSource).GetProperty(propertyName)),
            //new Expression[] { Expression.Constant("Chef") });
        }

        private static Func<TSource, bool> GetFunc<TSource,TParam>(string propertyName, string propertyValue)
        {
            try
            {
                var func = GetExpression<TSource,TParam>(propertyName, propertyValue).Compile();  //only need to compiled expression
                return func;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static Expression<Func<T, bool>> ContainsFilterExpression<T>(string propertyValue,string propertyName)
        {
            var parameterExpression = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameterExpression, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var someValue = Expression.Constant(propertyValue);
            var containsMethodExpression = Expression.Call(someValue, method, propertyExpression);
            var lambdaExpression = Expression.Lambda<Func<T, bool>>(containsMethodExpression, parameterExpression);
            return lambdaExpression;
        }
    }
}
