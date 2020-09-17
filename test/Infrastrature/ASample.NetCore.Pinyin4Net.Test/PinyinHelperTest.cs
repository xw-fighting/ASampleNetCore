using ASample.NetCore.Pinyin4Net.Format.Models.GwoyeuRomatzyh;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ASample.NetCore.Pinyin4Net.Test
{
    [TestClass]
    public class PinyinHelperTest
    {
        [TestMethod]
        public void ToHanyuPinyinStringArrayTest()
        {
            var hanzi = "缪可欣";
            var str = PinyinServices.ToHanyuPinyinString(hanzi);

            Console.WriteLine(str);
        }

        [TestMethod]
        public void HanyuToRomanizationStringTest()
        {
            var hanzi = "测试";
            var str = PinyinServices.HanyuToRomanizationString(hanzi,PinyinRomanizationType.TONGYONG_PINYIN);
            Console.WriteLine(str);
        }

        [TestMethod]
        public void HanyuToGwoyeuRomatzyhStringTest()
        {
            var hanzi = "测试";
            var str = PinyinServices.HanyuToGwoyeuRomatzyhString(hanzi);
            Console.WriteLine(str);
        }
    }
}
