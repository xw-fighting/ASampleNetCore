using System.Collections.Generic;


namespace ASample.NetCore.Services.Orders.Dtos
{
    public class OrderDetailDto : OrderDto
    {
        public CustomerDto Customer { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; }
    }
}
