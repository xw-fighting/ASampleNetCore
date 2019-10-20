using ASample.NetCore.Log;
using ASample.NetCore.Pinyin4Net.Convert;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ASample.NetCore.Pinyin4Net
{
    public class ChineseToPinyinConvert
    {
        /**
         * A hash table contains <Unicode, HanyuPinyin> pairs
         */
        private ConcurrentDictionary<string, string> UnicodeToHanyuPinyinTable = new ConcurrentDictionary<string, string>();

        private  ChineseToPinyinConvert()
        {
            InitializeResource();
        }

        private static  ChineseToPinyinConvert chineseToPinyinConvert;

        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static ChineseToPinyinConvert GetInstance()
        {
            // 如果类的实例不存在则创建，否则直接返回
            if (chineseToPinyinConvert == null)
            {
                chineseToPinyinConvert = new ChineseToPinyinConvert();
            }
            return chineseToPinyinConvert;
        }

        public void InitializeResource()
        {
            try
            {
                var resourceName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PinyinDb/unicode_to_hanyu_pinyin.txt");

                var table = File.ReadLines(resourceName)
                                .AsParallel()
                                .Select(l => l.Split(' '))
                                .ToDictionary(l => l[0], l => l[1]);
                UnicodeToHanyuPinyinTable = new ConcurrentDictionary<string, string>(table);
            }
            catch (FileNotFoundException ex)
            {
                FileLogServices.WriteLogToFile(ex.Message);
            }
            catch (IOException ex)
            {
                FileLogServices.WriteLogToFile(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public string GetHanyuPinyinRecordFromChar(char ch)
        {
            // convert Chinese character to code point (integer)
            // please refer to http://www.unicode.org/glossary/#code_point
            // Another reference: http://en.wikipedia.org/wiki/Unicode
            int codePointOfChar = ch;
            var codepointHexStr = codePointOfChar.ToString("x").ToUpper();

            // fetch from hashtable
            var dic = UnicodeToHanyuPinyinTable;
            if (dic.ContainsKey(codepointHexStr))
            {
                var foundRecord = dic[codepointHexStr];
                if (Util.IsValidRecord(foundRecord))
                    return foundRecord;
                return  null;
            }
            else
            {
                return null;
            }
        }

        public string [] GetHanyuPinyinStringArray(char ch)
        {
            var pinyinRecord = GetHanyuPinyinRecordFromChar(ch);

            if (!string.IsNullOrWhiteSpace(pinyinRecord))
            {
                var indexOfLeftBracket = pinyinRecord.IndexOf(Field.LEFT_BRACKET);
                var indexOfRightBracket = pinyinRecord.LastIndexOf(Field.RIGHT_BRACKET);

                var stripedString = pinyinRecord.Substring(indexOfLeftBracket + 1, indexOfRightBracket - 1);

                return stripedString.Split(Field.COMMA);
            }
            else
                return null; // no record found or mal-formatted record
        }

    }
}
