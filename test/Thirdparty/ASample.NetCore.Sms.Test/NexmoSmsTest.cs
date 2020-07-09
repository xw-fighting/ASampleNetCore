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
        public void NexmoSmsSendMsgTest()
        {
            var smsParam = new SmsParam
            {
                FromUser = "Vonage APIs",
                ToUser = "8618079652704",//8618879628727
                Message = "���,����Ϸ�����ƶ����ƣ��Żݵ�����",
            };

            var result =  NexmoServices.SendMsgAsync(smsParam).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }


        [TestMethod]
        public void NexmoSmsSendUnicodeMsgTest()
        {
            var smsParam = new SmsParam
            {
                FromUser = "Vonage APIs",
                ToUser = "8618770642594",//8618879628727
                Message = "��ã���ӭ�����ƶ�qi pai",
            };

            var result = NexmoServices.SendUnicodeMsgAsync(smsParam).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
