using System;

namespace ASample.NetCore.Services.Apis.Dtos.Orders
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
