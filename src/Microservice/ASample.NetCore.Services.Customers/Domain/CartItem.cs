using ASample.NetCore.Domain.AggregateRoots;
using System;

namespace ASample.NetCore.Services.Customers.Domain
{
    public class CartItem:AggregateRoot
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice => Quantity * UnitPrice;


        public CartItem(Product product, int quantity)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            UnitPrice = product.Price;
            Quantity = quantity;
        }

        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void Update(Product product)
        {
            ProductName = product.Name;
            UnitPrice = product.Price;
        }
    }
}
