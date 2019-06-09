using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Customers.Messages.Events.Orders
{
    [MessageNamespace("orders")]
    public class OrderApprovedEvent:IEvent
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        [JsonConstructor]
        public OrderApprovedEvent(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
