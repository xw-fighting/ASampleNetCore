using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASample.NetCore.Test.ExtensionTest
{
    [TestClass]
    public class LinqExtensionTest
    {
        [TestMethod]
        public  void WhereLingTest()
        {
            var list = new List<string>();
            list.Add("test");
            list.Add("test2");
            list.Add("test3");

            var result = list.Where(c => c == "test");
            Assert.IsNotNull(result);
        }
    }
}
