using ASample.NetCore.Services.Orders.Dtos;
using RestEase;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Orders.Services
{
    public interface IProductsService
    {
        [AllowAnyStatusCode]
        [Get("products/{id}")]
        Task<OrderItemDto> GetAsync([Path] Guid id);
    }
}
