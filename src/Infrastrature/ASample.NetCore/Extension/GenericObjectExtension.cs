
namespace ASample.NetCore.Extension
{
    public static class GenericObjectExtension
    {
        public static TEntity SetProperty<TEntity,TPropertyType>(this TEntity entity, TPropertyType propertyType, string propertyName = null)
        {
            var type = typeof(TEntity);
            var properties = type.GetProperties();//获取到泛型所有属性的集合

            foreach (var pi in properties)
            {
                if (pi.Name == propertyName && !string.IsNullOrEmpty(propertyName))
                {
                    pi.SetValue(entity, propertyType, null);//给泛型的属性赋值
                    break;
                }
                if (pi.PropertyType.Equals(typeof(TPropertyType)))//判断属性的类型是不是DbOptions
                {
                    pi.SetValue(entity, propertyType, null);//给泛型的属性赋值
                    break;
                }
            }
            return entity;
        }

        public static TModle GetPropertyValue<TEntity,TModle>(this TEntity entity, string propertyName = null)
            where TModle : class,new()
        {
            var type = entity.GetType();
            var properties = type.GetProperties();//获取到泛型所有属性的集合
            var value = new TModle();
            foreach (var pi in properties)
            {
                if (pi.Name == propertyName && !string.IsNullOrEmpty(propertyName))
                {
                    value = (TModle)pi.GetValue(entity);//给泛型的属性赋值
                    break;
                }
                if (pi.PropertyType.Equals(typeof(TModle)))//判断属性的类型是不是DbOptions
                {
                    value = (TModle)pi.GetValue(entity);//给泛型的属性赋值
                    break;
                }
            }
            return value;
        }
    }
}
