using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Services.Apis.Dtos.Orders;
using ASample.NetCore.Services.Apis.Queries;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Apis.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IOrdersService
    {
        [AllowAnyStatusCode]
        [Get("orders/{id}")]
        Task<OrderDetails> GetAsync([Path] Guid id);

        [AllowAnyStatusCode]
        [Get("customers/{customerId}/orders/{orderId}")]
        Task<OrderDetails> GetAsync([Path] Guid customerId, [Path] Guid orderId);

        [AllowAnyStatusCode]
        [Get("orders")]
        Task<PagedResult<OrderDto>> BrowseAsync([Query] BrowseOrders query);
    }
}
