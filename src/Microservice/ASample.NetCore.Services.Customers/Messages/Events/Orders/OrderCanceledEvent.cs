using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Customers.Messages.Events.Orders
{
    [MessageNamespace("orders")]

    public class OrderCancelEvent:IEvent
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        [JsonConstructor]
        public OrderCancelEvent(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
