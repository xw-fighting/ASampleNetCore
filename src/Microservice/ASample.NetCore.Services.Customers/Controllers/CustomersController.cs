using System.Threading.Tasks;
using ASample.NetCore.Dispatchers;
using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Services.Customers.Dtos;
using ASample.NetCore.Services.Customers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.Services.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseController
    {
        public CustomersController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<CustomerDto>>> Get([FromQuery] BrowseCustomers query)
            => Collection(await QueryAsync(query));

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> Get([FromRoute] GetCustomer query)
            => Single(await QueryAsync(query));
    }
}