using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Products.Messages.Events
{
    public class ProductUpdatedEvent:IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Vender { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [JsonConstructor]
        public ProductUpdatedEvent(Guid id, string name, string description, string vender, int quantity, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Vender = vender;
            Quantity = quantity;
            Price = price;
        }
    }
}
