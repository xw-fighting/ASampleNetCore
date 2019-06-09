using ASample.NetCore.Domain;
using ASample.NetCore.Services.Orders.Dtos;
using System;

namespace ASample.NetCore.Services.Orders.Queries
{
    public class GetOrder : IQuery<OrderDetailsDto>
    {
        public Guid OrderId { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
