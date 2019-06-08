using ASample.NetCore.Domain;
using ASample.NetCore.Services.Products.Dtos;
using System;

namespace ASample.NetCore.Services.Products.Queries
{
    public class GetProduct:IQuery<ProductDto>
    {
        public Guid Id { get; set; }
    }
}
