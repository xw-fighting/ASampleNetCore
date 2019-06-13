using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Apis.Messages.Commands.Customers
{
    [MessageNamespace("customers")]
    public class AddProductToCartCommand:ICommand
    {
        public Guid CustomerId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        [JsonConstructor]
        public AddProductToCartCommand(Guid customerId, Guid productId,
            int quantity)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
