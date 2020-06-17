using System;
using ASample.NetCore.Sms.Nexmos;
using ASample.NetCore.Sms.Nexmos.Params;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NUnit.Framework.Internal;

namespace ASample.NetCore.Sms.Test
{
    [TestClass]
    public class NexmoSmsTest
    {
        [TestMethod]
        public void NexmoSmsSendTest()
        {
            var smsParam = new SmsParam
            {
                FromUser = "18079605966",
                ToUser = "18079652704",
                Message = "hello"
            };

            var result =  NexmoServices.SendMsgAsync(smsParam).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
