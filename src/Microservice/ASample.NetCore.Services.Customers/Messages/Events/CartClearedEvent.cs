using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
