using System.Threading.Tasks;
using ASample.NetCore.Dispatchers;
using ASample.NetCore.Services.Customers.Dtos;
using ASample.NetCore.Services.Customers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.Services.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : BaseController
    {
        public CartsController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> Get([FromRoute] GetCart query)
            => Single(await QueryAsync(query));
    }
}
