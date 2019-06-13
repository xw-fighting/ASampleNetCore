using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Apis.Messages.Commands.Orders
{
    [MessageNamespace("orders")]
    public class ApproveOrderCommand:ICommand
    {
        public Guid Id { get; set; }

        [JsonConstructor]
        public ApproveOrderCommand(Guid id)
        {
            Id = id;
        }
    }
}
