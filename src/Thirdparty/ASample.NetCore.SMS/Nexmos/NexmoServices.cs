using System.Threading.Tasks;
using ASample.NetCore.Sms.Nexmos.Params;
using Nexmo.Api;
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
                text = param.Message
            });
            return await Task.FromResult(results);
        }
    }
}
