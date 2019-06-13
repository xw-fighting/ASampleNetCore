using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Apis.Messages.Commands.Orders
{
    [MessageNamespace("orders")]
    public class CreateOrderCommand:ICommand
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        [JsonConstructor]
        public CreateOrderCommand(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
