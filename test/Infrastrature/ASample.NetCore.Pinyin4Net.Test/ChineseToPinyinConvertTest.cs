using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ASample.NetCore.Pinyin4Net.Test
{
    [TestClass]
    public class ChineseToPinyinConvertTest
    {
        [TestMethod]
        public void GetHanyuPinyinRecordFromCharTest()
        {
            var hanzi = "≤‚ ‘";
            var chineseToPinyinConverter = ChineseToPinyinConvert.GetInstance();
            foreach (var item in hanzi)
            {
                var t = chineseToPinyinConverter.GetHanyuPinyinRecordFromChar(item);
                Console.WriteLine(t);
            }
        }

        [TestMethod]
        public void GetHanyuPinyinStringArrayTest()
        {
            var hanzi = "≤‚ ‘";
            var chineseToPinyinConverter = ChineseToPinyinConvert.GetInstance();
            foreach (var item in hanzi)
            {
                var t = chineseToPinyinConverter.GetHanyuPinyinStringArray(item);
                Console.WriteLine(t);
            }
        }

       
    }
}
