using System;
using System.Threading.Tasks;
using ASample.NetCore.Mvc;
using ASample.NetCore.RabbitMq.Publisher;
using ASample.NetCore.Services.Apis.Messages.Commands.Orders;
using ASample.NetCore.Services.Apis.Queries;
using ASample.NetCore.Services.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace ASample.NetCore.Services.Apis.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService, IBusPublisher busPublisher, ITracer tracer) : base(busPublisher, tracer)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]BrowseOrders query)
        {
            var result = Collection(await _ordersService.BrowseAsync(query));
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = Single(await _ordersService.GetAsync(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateOrderCommand command)
        {
            var result = await SendAsync(command.Bind(c => c.Id, UserId), resourceId: command.Id, resource: "orders");
            return result;
        }

        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveAsync(ApproveOrderCommand command)
        {
            var result = await SendAsync(command.Bind(c => c.Id, UserId), resourceId: command.Id, resource: "orders");
            return result;
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> Complete(CompleteOrderCommand command)
        {
            var result = await SendAsync(command.Bind(c => c.Id, UserId), resourceId: command.Id, resource: "orders");
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await SendAsync(new CancelOrderCommand(id,UserId));
            return result;
        }
    }
}