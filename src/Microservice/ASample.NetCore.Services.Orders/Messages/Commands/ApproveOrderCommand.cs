using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Orders.Messages.Commands
{
    public class ApproveOrderCommand : ICommand
    {
        public Guid Id { get; }

        [JsonConstructor]
        public ApproveOrderCommand(Guid id)
        {
            Id = id;
        }
    }
}
