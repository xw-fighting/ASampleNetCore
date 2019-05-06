using ASample.NetCore.Configuration;
using ASample.NetCore.Test.Model.ConfigReaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASample.NetCore.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

    }

    [TestClass]
    public class ConfigReaderTest
    {
        [TestMethod]
        public void GetGetValue()
        {
            //var config = new ConfigTestModel();
            var result = ConfigurationReader.GetValue<ConfigTestModel>("test");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetSection()
        {
            //var config = new ConfigTestModel();
            var result = ConfigurationReader.GetSection("test:name");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetValue()
        {
            //var config = new ConfigTestModel();
            var result = ConfigurationReader.GetValue("test:name");
            Assert.IsNotNull(result);
        }
    }
}
