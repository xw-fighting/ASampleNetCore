using System.Threading.Tasks;
using ASample.NetCore.Mvc;
using ASample.NetCore.RabbitMq.Publisher;
using ASample.NetCore.Services.Apis.Messages.Commands.Customers;
using ASample.NetCore.Services.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace ASample.NetCore.Services.Apis.Controllers
{
    public class CartsController : BaseController
    {
        private readonly ICustomersService _customersService;

        protected CartsController(ICustomersService customersService,IBusPublisher busPublisher, ITracer tracer) : base(busPublisher, tracer)
        {
            _customersService = customersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = Single(await _customersService.GetCartAsync(UserId));
            return result;
        }

        [HttpPost("items")]
        public async Task<IActionResult> PostAsync(AddProductToCartCommand command)
        {
            var result = await SendAsync(command.Bind(c => c.CustomerId, UserId), resourceId: command.CustomerId, resource: "carts");
            return result;
        }

        [HttpDelete]
        public async Task<IActionResult> Clear()
        {
            var result = await SendAsync(new ClearCartCommand(UserId));
            return result;
        }

        [HttpDelete("items/{productId}")]
        public async Task<IActionResult> DeleteProductAsync(DeleteProductFromCartCommand command)
        {
            var result = await SendAsync(new DeleteProductFromCartCommand(command.CustomerId, command.ProductId));
            return result;
        }

    }
}