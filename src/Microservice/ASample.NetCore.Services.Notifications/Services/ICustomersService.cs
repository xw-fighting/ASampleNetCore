using ASample.NetCore.Services.Notifications.Dtos;
using RestEase;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Notifications.Services
{
    public interface  ICustomersService
    {
        [AllowAnyStatusCode]
        [Get("customers/{id}")]
        Task<CustomerDto> GetAsync([Path] Guid id);
    }
}
