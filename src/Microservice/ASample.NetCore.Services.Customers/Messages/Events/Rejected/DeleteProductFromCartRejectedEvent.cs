using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Customers.Messages.Events.Rejected
{
    public class DeleteProductFromCartRejectedEvent:IRejectedEvent
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }

        public string Code { get; set; }
        public string Reason { get; set; }

        [JsonConstructor]
        public DeleteProductFromCartRejectedEvent(Guid customerId, Guid productId, string code, string reason)
        {
            CustomerId = customerId;
            ProductId = productId;
            Code = code;
            Reason = reason;
        }
    }
}
