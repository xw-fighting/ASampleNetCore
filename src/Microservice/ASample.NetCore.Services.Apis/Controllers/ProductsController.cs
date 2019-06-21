using System;
using System.Threading.Tasks;
using ASample.NetCore.Mvc;
using ASample.NetCore.RabbitMq.Publisher;
using ASample.NetCore.Services.Apis.Framwork;
using ASample.NetCore.Services.Apis.Messages.Commands.Products;
using ASample.NetCore.Services.Apis.Queries;
using ASample.NetCore.Services.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace ASample.NetCore.Services.Apis.Controllers
{
    [AdminAuth]
    public class ProductsController : BaseController
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService,IBusPublisher busPublisher, ITracer tracer) : base(busPublisher, tracer)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]BrowseProducts query)
        {
            var result = Collection(await _productsService.BrowseAsync(query));
            return result;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = Single(await _productsService.GetAsync(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CreateProductCommand command)
        {
            var result = await SendAsync(command.Bind(c => c.Id, UserId),resourceId:command.Id,resource:"products");
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id,UpdateProductCommand command)
        {
            var result = await SendAsync(command.Bind(c => c.Id, UserId), resourcId: command.Id, resource: "products");
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await SendAsync(new DeleteProductCommand(id));
            return result;
        }
     }
}