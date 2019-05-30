using System;

namespace ASample.NetCore.Extension
{
    public static class DateTimeExtension
    {
        public static long ToTimestamp(this DateTime dateTime)
        {
            var centuryBegin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expectedDate = dateTime.Subtract(new TimeSpan(centuryBegin.Ticks));

            return expectedDate.Ticks / 10000;
        }

        public static string ToString(this DateTime dateTime,bool onlyDate = false)
        {
            var result = string.Empty;
            if (!onlyDate)
                result = dateTime.ToString("yyyy-MM-dd HH:mm:sss");
            else
                result = dateTime.ToString("yyyy-MM-dd");
            return result;
        }
    }
}
