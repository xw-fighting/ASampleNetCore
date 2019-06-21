using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.Mvc;
using ASample.NetCore.RabbitMq.Publisher;
using ASample.NetCore.Services.Apis.Framwork;
using ASample.NetCore.Services.Apis.Messages.Commands.Customers;
using ASample.NetCore.Services.Apis.Queries;
using ASample.NetCore.Services.Apis.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace ASample.NetCore.Services.Apis.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly ICustomersService _customersService;
        public  CustomersController(ICustomersService customersService, IBusPublisher busPublisher, ITracer tracer) : base(busPublisher, tracer)
        {
            _customersService = customersService;
        }

        [HttpGet]
        [AdminAuth]
        public async Task<IActionResult> Get([FromQuery] BrowseCustomers query)
           => Collection(await _customersService.BrowseAsync(query));

        [HttpGet("{id}")]
        [AdminAuth]
        public async Task<IActionResult> Get(Guid id)
            => Single(await _customersService.GetAsync(id), x => x.Id == UserId || IsAdmin);

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerCommand command)
        {
            var result = await SendAsync(command.Bind(c =>c.Id,UserId), resourceId: command.Id, resource: "customers");
            return result;
        }

    }
}