using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ASample.NetCore.Services.Products.Messages.Events
{
    public class ProductReleasedEvent:IEvent
    {
        public Guid OrderId { get; set; }
        public IDictionary<Guid, int> Products { get; }

        [JsonConstructor]
        public ProductReleasedEvent(Guid orderId, IDictionary<Guid, int> products)
        {
            OrderId = orderId;
            Products = products;
        }
    }
}
