using System;
using System.Collections.Generic;

namespace ASample.NetCore.Services.Orders.Dtos
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public IEnumerable<CartItemDto> Items { get; set; }
    }
}
