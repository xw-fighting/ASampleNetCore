using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Services.Apis.Dtos.Products;
using ASample.NetCore.Services.Apis.Queries;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Apis.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IProductsService
    {
        [AllowAnyStatusCode]
        [Get("products/{id}")]
        Task<ProductDto> GetAsync([Path] Guid id);

        [AllowAnyStatusCode]
        [Get("products")]
        Task<PagedResult<ProductDto>> BrowseAsync([Query] BrowseProducts query);
    }
}
