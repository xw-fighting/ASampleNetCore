using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using ASample.NetCore.Extension;

namespace ASample.NetCore.Test.Extension
{
    [TestClass]
    public class EnumExtensionTest
    {
        [TestMethod]
        public void GetDescTest()
        {
            var testEnum =  TestEnum.Test1;
            var result = testEnum.GetDesc();
            Assert.IsTrue(result != null && !string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void GetNameValue()
        {
            var testEnum = new TestEnum();
            var result = testEnum.GetNameValue();
            Assert.IsTrue(result != null && result.Count > 0);
        }
    }


    public enum TestEnum
    {
        [System.ComponentModel.Description("测试一")]
        Test1 = 0,
        [System.ComponentModel.Description("测试二")]
        Test2 = 1,
        [System.ComponentModel.Description("测试三")]
        Test3 = 2,
    }
}
