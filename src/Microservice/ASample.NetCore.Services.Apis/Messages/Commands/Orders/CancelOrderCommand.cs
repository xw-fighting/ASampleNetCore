using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Apis.Messages.Commands.Orders
{
    [MessageNamespace("orders")]
    public class CancelOrderCommand:ICommand
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        [JsonConstructor]
        public CancelOrderCommand(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
