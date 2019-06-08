using ASample.NetCore.Domain.Message;
using System;
using System.Collections.Generic;

namespace ASample.NetCore.Services.Products.Messages.Commands
{
    public class ReleaseProductCommand:ICommand
    {
        public Guid OrderId { get; set; }
        public IDictionary<Guid, int> Products { get; }

        public ReleaseProductCommand(Guid orderId, IDictionary<Guid, int> products)
        {
            OrderId = orderId;
            Products = products;
        }
    }
}
