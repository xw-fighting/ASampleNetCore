using System.Threading.Tasks;
using ASample.NetCore.Sms.Nexmos.Params;
using Nexmo.Api;
using Nexmo.Api.Messaging;
using Nexmo.Api.Request;

namespace ASample.NetCore.Sms.Nexmos
{
    public static class NexmoServices
    {
        public static async Task<Nexmo.Api.SMS.SMSResponse> SendMsgAsync(SmsParam param)
        {
            var client = new Client(new Credentials
            {
                ApiKey = "a647a64e",
                ApiSecret = "wT0beoytSXjAofU0"
            });

            var results = client.SMS.Send(new Nexmo.Api.SMS.SMSRequest
            {
                from = param.FromUser,
                to = param.ToUser,
                text = param.Message,
            });
            return await Task.FromResult(results);
        }


        public static async Task<SendSmsResponse> SendUnicodeMsgAsync(SmsParam param)
        {
            var client = new NexmoClient(new Credentials
            {
                ApiKey = "a647a64e",
                ApiSecret = "wT0beoytSXjAofU0"
            });

            var response = client.SmsClient.SendAnSms(new SendSmsRequest()
            {
                From = param.FromUser,
                To = param.ToUser,
                Text = param.Message,
                Type = SmsType.unicode,
                
            });
            return await Task.FromResult(response);
        }
    }
}
