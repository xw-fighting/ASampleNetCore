using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Products.Messages.Events
{
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
