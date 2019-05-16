using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ASample.NetCore.Extension
{
    public static class EnumExtension
    {
        /// 扩展方法，获得枚举的Description
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <param name="nameInstend">当枚举没有定义DescriptionAttribute,是否用枚举名代替，默认使用</param>
        /// <returns>枚举的Description</returns>
        public static string GetDesc(this Enum value, bool nameInstend = true)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            var field = type.GetField(name);
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute == null && nameInstend == true)
                return name;
            return attribute == null ? "" : attribute.Description;
        }

        public static Dictionary<int, string> GetNameValue(this Enum enumObj)
        {
            var dic = new Dictionary<int, string>();
            var type = enumObj.GetType();
            var names = Enum.GetNames(type);
            foreach (var name in names)
            {
                var value = GetValueByName(name, type);
                var desc = string.Empty;
                if (string.IsNullOrEmpty(name))
                    continue;
                var field = type.GetField(name);
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute == null)
                    desc = name;
                if (attribute != null)
                    desc = attribute.Description;
                dic.Add(value, desc);
            }

            return dic;
        }

        private static int GetValueByName(string name, Type type)
        {
            if (string.IsNullOrWhiteSpace(name))
                return -1;
            var str = -1;
            try
            {
                str = (int)Enum.Parse(type, name);
            }
            catch { }
            return str;
        }
    }
}
