using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ASample.NetCore.Services.Orders.Messages.Events
{
    public class OrderCanceledEvent:IEvent
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; }
        public IDictionary<Guid, int> Products { get; }

        [JsonConstructor]
        public OrderCanceledEvent(Guid id, Guid customerId, IDictionary<Guid, int> products)
        {
            Id = id;
            CustomerId = customerId;
            Products = products;
        }
    }
}
