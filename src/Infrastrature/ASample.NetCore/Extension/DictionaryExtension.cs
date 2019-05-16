using System.Collections.Generic;

namespace ASample.NetCore.Extension
{
    public static class DictionaryExtension
    {
        public static List<EnumDto> ToList(this Dictionary<int,string> dic)
        {
            var keys = dic.Keys;
            var dicList = new List<EnumDto>();
            foreach (var key in keys)
            {
                var value = string.Empty;
                if(dic.TryGetValue(key, out value))
                {
                    var enumDto = new EnumDto
                    {
                        Key = key,
                        Value = value
                    };
                    dicList.Add(enumDto);
                }
                continue;
            }
            return dicList;
        }
    }
}
