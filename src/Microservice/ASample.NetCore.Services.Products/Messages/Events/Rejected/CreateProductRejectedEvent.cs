using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Products.Messages.Events.Rejected
{
    public class CreateProductRejectedEvent:IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public CreateProductRejectedEvent(Guid id, string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}
