using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Customers.Messages.Commands
{
    public class ClearCartCommand:ICommand
    {
        public Guid CustomerId { get; set; }

        [JsonConstructor]
        public ClearCartCommand(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
