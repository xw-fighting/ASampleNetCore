﻿using ASample.NetCore.Domain;
using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Services.Orders.Dtos;
using System;

namespace ASample.NetCore.Services.Orders.Queries
{
    public class BrowseOrders : PagedQueryBase, IQuery<PagedResult<OrderDto>>
    {
        public Guid CustomerId { get; set; }
    }
}
