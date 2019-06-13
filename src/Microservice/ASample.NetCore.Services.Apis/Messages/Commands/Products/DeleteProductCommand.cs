using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Apis.Messages.Commands.Products
{
    [MessageNamespace("products")]
    public class DeleteProductCommand:ICommand
    {
        public Guid Id { get; set; }

        [JsonConstructor]
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
