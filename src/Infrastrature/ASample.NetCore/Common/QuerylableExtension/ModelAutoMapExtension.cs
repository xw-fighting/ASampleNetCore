using System;
using System.Collections.Generic;
using System.Text;

namespace ASample.NetCore.Common.QuerylableExtension
{
    public static class ModelAutoMapExtension
    {
        public static TResult EntityMap<TSource,TResult>(this TSource source,TResult result)
        {
            //获取TSource property
            return result;
        }

    }
}
