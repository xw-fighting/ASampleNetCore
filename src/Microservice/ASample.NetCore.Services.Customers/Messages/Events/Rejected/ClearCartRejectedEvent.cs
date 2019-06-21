using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Customers.Messages.Events.Rejected
{
    public class ClearCartRejectedEvent: IRejectedEvent
    {
        public Guid CustomerId { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public ClearCartRejectedEvent(Guid customerId, string reason, string code)
        {
            CustomerId = customerId;
            Reason = reason;
            Code = code;
        }
    }
}
