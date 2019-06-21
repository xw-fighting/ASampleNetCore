using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Orders.Messages.Events
{
    public class OrderCompletedEvent:IEvent
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        [JsonConstructor]
        public OrderCompletedEvent(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
