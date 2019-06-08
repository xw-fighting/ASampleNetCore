using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ASample.NetCore.Services.Products.Messages.Commands
{
    public class ReserveProductCommand:ICommand
    {
        public Guid OrderId { get; set; }
        public IDictionary<Guid, int> Products { get; }

        [JsonConstructor]
        public ReserveProductCommand(Guid orderId, IDictionary<Guid, int> products)
        {
            OrderId = orderId;
            Products = products;
        }
    }
}
