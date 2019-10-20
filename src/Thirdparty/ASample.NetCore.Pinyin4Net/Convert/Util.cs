
namespace ASample.NetCore.Pinyin4Net.Convert
{
    public class Util
    {
        public static bool IsValidRecord(string record)
        {
            var noneStr = "(none0)";

            if ((null != record) && !record.Equals(noneStr)
                    && record.StartsWith(Field.LEFT_BRACKET.ToString())
                    && record.EndsWith(Field.RIGHT_BRACKET.ToString()))
            {
                return true;
            }
            else
                return false;
        }


        /**
         * @param hanyuPinyinWithToneNumber
         * @return Hanyu Pinyin string without tone number
         */
        public static string ExtractToneNumber(string hanyuPinyinWithToneNumber)
        {
            return hanyuPinyinWithToneNumber.Substring(hanyuPinyinWithToneNumber.Length - 1);
        }

        /**
         * @param hanyuPinyinWithToneNumber
         * @return only tone number
         */
        public static string ExtractPinyinString(string hanyuPinyinWithToneNumber)
        {
            return hanyuPinyinWithToneNumber.Substring(0, hanyuPinyinWithToneNumber.Length - 1);
        }

        /// <summary>
        /// 判断字符是否是汉字
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsHanzi(char ch)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ch.ToString(), @"[\u4e00-\u9fbb]");
        }
    }
}
