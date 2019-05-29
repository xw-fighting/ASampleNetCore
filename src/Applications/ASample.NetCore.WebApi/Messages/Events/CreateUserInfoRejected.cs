using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;
namespace ASample.NetCore.WebApi.Messages.Events
{
    public class CreateUserInfoRejected: IRejectedEvent
    {
        public Guid CustomerId { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public CreateUserInfoRejected(Guid customerId, string reason, string code)
        {
            CustomerId = customerId;
            Reason = reason;
            Code = code;
        }
    }
}
