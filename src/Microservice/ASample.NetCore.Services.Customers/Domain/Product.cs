using ASample.NetCore.Domain.AggregateRoots;
using ASample.NetCore.Domain.Exceptions;
using System;

namespace ASample.NetCore.Services.Customers.Domain
{
    public class Product : AggregateRoot
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public Product(Guid id,string name, decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public void SetQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new ASampleException("invalid_product_quantity",
                    "Product quantity cannot be negative.");
            }
            Quantity = quantity;
        }
    }
}
