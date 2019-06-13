using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Apis.Messages.Commands.Customers
{
    [MessageNamespace("customers")]
    public class DeleteProductFromCartCommand:ICommand
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }

        [JsonConstructor]
        public DeleteProductFromCartCommand(Guid customerId, Guid productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }
    }
}
