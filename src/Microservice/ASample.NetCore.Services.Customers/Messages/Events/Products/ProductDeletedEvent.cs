using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Customers.Messages.Events.Products
{
    [MessageNamespace("products")]
    public class ProductDeletedEvent:IEvent
    {
        public Guid Id { get; set; }

        [JsonConstructor]
        public ProductDeletedEvent(Guid id)
        {
            Id = id;
        }
    }
}
