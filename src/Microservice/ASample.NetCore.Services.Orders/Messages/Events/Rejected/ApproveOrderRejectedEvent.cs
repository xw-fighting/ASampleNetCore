using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;
namespace ASample.NetCore.Services.Orders.Messages.Events.Rejected
{
    public class ApproveOrderRejectedEvent:IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public ApproveOrderRejectedEvent(Guid id, string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}
