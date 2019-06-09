using ASample.NetCore.Services.Orders.Dtos;
using RestEase;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Orders.Services
{
    public interface ICustomersService
    {
        [AllowAnyStatusCode]
        [Get("carts/{id}")]
        Task<CartDto> GetCartAsync([Path] Guid id);
    }
}
