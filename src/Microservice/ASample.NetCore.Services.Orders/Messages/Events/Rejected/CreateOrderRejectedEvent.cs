using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Orders.Messages.Events.Rejected
{
    public class CreateOrderRejectedEvent:IRejectedEvent
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public CreateOrderRejectedEvent(Guid id, Guid customerId, string reason, string code)
        {
            Id = id;
            CustomerId = customerId;
            Reason = reason;
            Code = code;
        }
    }
}
