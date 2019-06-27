using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Dispatchers;
using ASample.NetCore.Domain;
using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Services.Products.Dtos;
using ASample.NetCore.Services.Products.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASample.NetCore.Services.Products.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        public ProductsController(IDispatcher dispatcher) : base(dispatcher)
        {}

        [HttpGet("{id}")]
        public async Task<ProductDto> GetAsync([FromRoute] GetProduct query)
        {
            var result = await QueryAsync(query);
            return result;
        }

        [HttpGet]
        public async Task<PagedResult<ProductDto>> QueryPagedAsync([FromQuery]QueryProducts query)
        {
            var result = await QueryAsync(query);
            return result;
        }

    }
}