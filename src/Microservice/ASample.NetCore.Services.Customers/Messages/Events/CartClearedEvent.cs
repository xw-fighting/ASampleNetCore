using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Customers.Messages.Events
{
    public class CartClearedEvent:IEvent
    {
        public Guid CustomerId { get; set; }

        [JsonConstructor]
        public CartClearedEvent(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
