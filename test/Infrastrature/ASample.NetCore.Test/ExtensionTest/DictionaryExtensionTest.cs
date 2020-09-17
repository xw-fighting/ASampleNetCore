using System.Linq;
using ASample.NetCore.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
