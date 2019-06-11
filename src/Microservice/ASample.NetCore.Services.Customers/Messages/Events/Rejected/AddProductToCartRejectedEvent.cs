using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Customers.Messages.Events.Rejected
{
    public class AddProductToCartRejectedEvent: IRejectedEvent
    {
        public Guid CustomerId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public AddProductToCartRejectedEvent(Guid customerId, Guid productId,
            int quantity, string reason, string code)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
            Reason = reason;
            Code = code;
        }
    }
}
