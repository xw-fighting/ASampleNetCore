using ASample.NetCore.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Orders.Domain
{
    public class OrderItem:AggregateRoot
    {
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        public OrderItem(Guid id,string name, int quantity, decimal unitPrice)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
