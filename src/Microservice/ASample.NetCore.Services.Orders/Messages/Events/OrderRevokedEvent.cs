using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Orders.Messages.Events
{
    public class OrderRevokedEvent:IEvent
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        [JsonConstructor]
        public OrderRevokedEvent(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
