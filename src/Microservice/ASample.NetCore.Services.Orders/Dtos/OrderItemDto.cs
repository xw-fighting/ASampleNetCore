using System;

namespace ASample.NetCore.Services.Orders.Dtos
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
