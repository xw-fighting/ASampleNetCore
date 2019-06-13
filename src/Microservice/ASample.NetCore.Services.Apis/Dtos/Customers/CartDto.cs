using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Apis.Dtos.Customers
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public IList<CartItemDto> Items { get; set; } = new List<CartItemDto>();
        public decimal TotalPrice => Items.Sum(x => x.TotalPrice);
    }
}
