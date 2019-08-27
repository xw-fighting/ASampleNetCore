using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASample.NetCore.Common.QuerylableExtension
{
    public static class ModelAutoMapExtension
    {
        //public static TResult EntityMap<TSource,TResult>(this TSource source,TResult result)
        //{
        //    //获取TSource property
        //    return result;
        //}

        public static TResult EntityMap<TSource, TResult>(this TSource entity)
            where TResult : class, new()
        {
            var source = entity.GetType();
            var tResult = new TResult();
            var result = tResult.GetType();
            var sourcePros = source.GetProperties();
            var resultPros = result.GetProperties();
            foreach (var sourcePro in sourcePros)
            {
                var resultPro = resultPros.FirstOrDefault(c => c.Name == sourcePro.Name);
                if (resultPro != null)
                {
                    try
                    {
                        var value = sourcePro.GetValue(entity);
                        //if (sourcePro.PropertyType == typeof(DateTime) && resultPro.PropertyType == typeof(string))
                        //{
                        //    DateTime.Parse(value.ToString()).ToString("")
                        //}

                        resultPro.SetValue(tResult, value);
                    }
                    catch
                    { }
                }
            }
            return tResult;
        }

    }
}
