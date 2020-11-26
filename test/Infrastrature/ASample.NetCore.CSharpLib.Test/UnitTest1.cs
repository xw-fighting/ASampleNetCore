using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Client;

namespace ASample.NetCore.CSharpLib.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var test = new Test2();
            var res = test.Publish();
            
        }
    }

    public class Test2
    {
        private static string _ConnectionFactory = null;
        public Test2()
        {
            _ConnectionFactory = Test1.con;
        }

        public string Publish()
        {
            return "123";
        }
    }

    public class Test1
    {
        private  static Dictionary<string,string> dic = new Dictionary<string,string>();

        public static string con
        {
            get
            {
                var key = "test";
                if (!dic.ContainsKey(key) || ReferenceEquals(dic[key], null))
                {
                    dic.Add(key, "123");
                }

                return dic[key];
            }
        }

    }
}
