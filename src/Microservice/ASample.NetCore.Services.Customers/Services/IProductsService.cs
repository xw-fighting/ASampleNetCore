using ASample.NetCore.Services.Customers.Dtos;
using RestEase;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Services
{
    public interface IProductsService
    {
        [AllowAnyStatusCode]
        [Get("products/{id}")]
        Task<ProductDto> GetAsync([Path] Guid id);
    }
}
