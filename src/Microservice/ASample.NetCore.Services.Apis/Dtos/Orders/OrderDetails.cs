using ASample.NetCore.Services.Apis.Dtos.Customers;
using System.Collections.Generic;

namespace ASample.NetCore.Services.Apis.Dtos.Orders
{
    public class OrderDetails:OrderDto
    {
        public CustomerDto Customer { get; set; }

        public IEnumerable<OrderItem> Items { get; set; }
    }
}
