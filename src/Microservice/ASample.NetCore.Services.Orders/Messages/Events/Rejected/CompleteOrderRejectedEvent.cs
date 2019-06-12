using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Orders.Messages.Events.Rejected
{
    public class CompleteOrderRejectedEvent:IRejectedEvent
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public string Code { get; set; }

        public string Reason { get; set; }

        [JsonConstructor]
        public CompleteOrderRejectedEvent(Guid id, Guid customerId, string code, string reason)
        {
            Id = id;
            CustomerId = customerId;
            Code = code;
            Reason = reason;
        }
    }
}
