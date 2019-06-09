using ASample.NetCore.Domain;
using ASample.NetCore.Services.Customers.Dtos;
using System;

namespace ASample.NetCore.Services.Customers.Queries
{
    public class GetCart:IQuery<CartDto>
    {
        public Guid Id { get; set; }
    }
}
