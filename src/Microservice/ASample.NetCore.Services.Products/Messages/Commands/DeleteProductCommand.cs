using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Products.Messages.Commands
{
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
