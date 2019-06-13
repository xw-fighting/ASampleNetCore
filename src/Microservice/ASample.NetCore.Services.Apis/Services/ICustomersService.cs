using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Services.Apis.Dtos.Customers;
using ASample.NetCore.Services.Apis.Queries;
using RestEase;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Apis.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface ICustomersService
    {
        [AllowAnyStatusCode]
        [Get("customers/{id}")]
        Task<CustomerDto> GetAsync([Path] Guid id);

        [AllowAnyStatusCode]
        [Get("customers")]
        Task<PagedResult<CustomerDto>> BrowseAsync([Query] BrowseCustomers query);

        [AllowAnyStatusCode]
        [Get("carts/{id}")]
        Task<CartDto> GetCartAsync([Path] Guid id);
    }
}
