using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Apis.Messages.Commands.Customers
{
    [MessageNamespace("customers")]
    public class ClearCartCommand:ICommand
    {
        public Guid CustomerId { get; }

        [JsonConstructor]
        public ClearCartCommand(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
