using ASample.NetCore.Pinyin4Net.Convert;
using ASample.NetCore.Pinyin4Net.Format;
using ASample.NetCore.Pinyin4Net.Format.Models.GwoyeuRomatzyh;

namespace ASample.NetCore.Pinyin4Net
{
    public class PinyinHelper
    {
        /// <summary>
        /// 将汉字转换伟拼音
        /// </summary>
        /// <param name="hanziStr">待转换汉字</param>
        /// <param name="hanyuPinyinOutputFormat">转换格式</param>
        /// <returns></returns>
        public static string ToHanyuPinyinStringArray(string hanziStr, HanyuPinyinOutputFormat hanyuPinyinOutputFormat = null)
        {
            var pinyin = string.Empty;
            if(hanyuPinyinOutputFormat == null)
            {
                hanyuPinyinOutputFormat = new HanyuPinyinOutputFormat();
            }
            foreach (var hanzi in hanziStr)
            {
                if (Util.IsHanzi(hanzi))
                {
                    string[] pinyinStrArray = ChineseToPinyinConvert.GetInstance().GetHanyuPinyinStringArray(hanzi);
                    if (null != pinyinStrArray)
                    {
                        if (pinyinStrArray.Length == 1)
                        {
                            // 拼音间追加一个空格，这里如果是多间字，拼音可能不准确
                            pinyin += PinyinFormatter.FormatHanyuPinyin(pinyinStrArray[0], hanyuPinyinOutputFormat) + " ";
                        }
                        else
                        {
                            for (int i = 0; i < pinyinStrArray.Length; i++)
                            {
                                pinyinStrArray[i] = PinyinFormatter.FormatHanyuPinyin(pinyinStrArray[i], hanyuPinyinOutputFormat);
                            }
                            pinyin += "(" + string.Join(",", pinyinStrArray) + ") ";
                        }
                    }
                    else
                        return null;
                }
            }
            return pinyin;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hanziStr"></param>
        /// <param name="targetPinyinSystem"></param>
        /// <returns></returns>
        public static string HanyuToRomanizationString(string hanziStr, PinyinRomanizationType targetPinyinSystem)
        {
            var pinyin = string.Empty;
            var romanization = RomanizationConvert.GetInstance();
            foreach (var hanzi in hanziStr)
            {
                if (Util.IsHanzi(hanzi))
                {
                    string[] pinyinStrArray = ChineseToPinyinConvert.GetInstance().GetHanyuPinyinStringArray(hanzi);
                    
                    if (null != pinyinStrArray)
                    {
                        if (pinyinStrArray.Length == 1)
                        {
                            // 拼音间追加一个空格，这里如果是多间字，拼音可能不准确
                            pinyin += romanization.GetRomanizationString(pinyinStrArray[0], PinyinRomanizationType.HANYU_PINYIN, targetPinyinSystem) + " ";
                        }
                        else
                        {
                            string[] arr = null;
                            for (int i = 0; i < pinyinStrArray.Length; i++)
                            {
                                arr[i] = romanization.GetRomanizationString(pinyinStrArray[i], PinyinRomanizationType.HANYU_PINYIN, targetPinyinSystem);
                            }
                            pinyin += "(" + string.Join(",", arr) + ") ";
                        }
                    }
                    else
                        return null;
                }
            }
            return pinyin;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hanziStr"></param>
        /// <returns></returns>
        public static string HanyuToGwoyeuRomatzyhString(string hanziStr)
        {
            var pinyin = string.Empty;
            var romanization = GwoyeuRomatzyhConvert.GetInstance();
            foreach (var hanzi in hanziStr)
            {
                if (Util.IsHanzi(hanzi))
                {
                    string[] pinyinStrArray = ChineseToPinyinConvert.GetInstance().GetHanyuPinyinStringArray(hanzi);

                    if (null != pinyinStrArray)
                    {
                        if (pinyinStrArray.Length == 1)
                        {
                            // 拼音间追加一个空格，这里如果是多间字，拼音可能不准确
                            pinyin += romanization.GetGwoyeuRomatzyh(pinyinStrArray[0]) + " ";
                        }
                        else
                        {
                            string[] arr = null;
                            for (int i = 0; i < pinyinStrArray.Length; i++)
                            {
                                arr[i] = romanization.GetGwoyeuRomatzyh(pinyinStrArray[i]);
                            }
                            pinyin += "(" + string.Join(",", arr) + ") ";
                        }
                    }
                    else
                        return null;
                }
            }
            return pinyin;

        }
    }
}
