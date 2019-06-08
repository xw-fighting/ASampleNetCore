using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Products.Messages.Events.Rejected
{
    public class UpdateProductRejectedEvent : IRejectedEvent
    {
        public Guid Id { get; set; }

        public string Code { get; set; }
        public string Reason { get; set; }

        [JsonConstructor]
        public UpdateProductRejectedEvent(Guid id, string code, string reason)
        {
            Id = id;
            Code = code;
            Reason = reason;
        }
    }
}
