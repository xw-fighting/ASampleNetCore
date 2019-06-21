using System.Threading.Tasks;
using ASample.NetCore.Dispatchers;
using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Services.Orders.Dtos;
using ASample.NetCore.Services.Orders.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.Services.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public OrdersController(IDispatcher dispatcher):base(dispatcher)
        {
        }

        [HttpGet("orders")]
        public async Task<PagedResult<OrderDto>> Get([FromQuery] BrowseOrders query)
        {
            var result = await QueryAsync(query);
            return result;
        }

        [HttpGet("orders/{orderId}")]
        public async Task<OrderDetailDto> Get([FromRoute] GetOrder query)
        {
            var result = await QueryAsync(query);
            return result;
        }

        [HttpGet("customers/{customerId}/orders/{orderId}")]
        public async Task<OrderDetailDto> GetForCustomer([FromRoute] GetOrder query)
        {
            var result = await QueryAsync(query);
            return result;
        }

    }
}