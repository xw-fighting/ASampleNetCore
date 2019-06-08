using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Products.Messages.Events.Rejected
{
    public class ReserveProductsRejectedEvent : IRejectedEvent
    {
        public Guid OrderId { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public ReserveProductsRejectedEvent(Guid orderId, string reason, string code)
        {
            OrderId = orderId;
            Reason = reason;
            Code = code;
        }
    }
}
