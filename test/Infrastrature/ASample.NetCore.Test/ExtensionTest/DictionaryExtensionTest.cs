using ASample.NetCore.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASample.NetCore.Test.Extension
{
    [TestClass]
    public class DictionaryExtensionTest
    {
        [TestMethod]
        public void ToListTest()
        {
            var testEnum = new TestEnum();
            var result = testEnum.GetNameValue();
            var list = result.ToList();
            Assert.IsTrue(!string.IsNullOrEmpty(list.FirstOrDefault().Value));
        }
    }
}
