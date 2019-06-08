using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Products.Messages.Commands
{
    public class CreateProductCommand:ICommand
    {
        public Guid  Id { get; set; }
        public string Name { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [JsonConstructor]
        public CreateProductCommand(Guid id, string name, 
            string vendor, string description, 
            int quantity, decimal price)
        {
            Id = id;
            Name = name;
            Vendor = vendor;
            Description = description;
            Quantity = quantity;
            Price = price;
        }
    }
}
