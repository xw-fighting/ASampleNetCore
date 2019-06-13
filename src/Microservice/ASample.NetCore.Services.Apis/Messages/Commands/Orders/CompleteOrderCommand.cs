using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Apis.Messages.Commands.Orders
{
    [MessageNamespace("orders")]
    public class CompleteOrderCommand:ICommand
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        [JsonConstructor]
        public CompleteOrderCommand(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
