using System;
using System.Linq;

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
    }
}
