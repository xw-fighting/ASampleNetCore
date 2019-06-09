using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Customers.Messages.Events
{
    public class ProductDeletedFromCartEvent:IEvent
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }

        [JsonConstructor]
        public ProductDeletedFromCartEvent(Guid customerId, Guid productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }
    }
}
