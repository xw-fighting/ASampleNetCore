using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Customers.Messages.Events.Rejected
{
    public class CreateCustomerRejectEvent:IRejectedEvent
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        public string Reason { get; set; }

        [JsonConstructor]
        public CreateCustomerRejectEvent(Guid id, string code, string reason)
        {
            Id = id;
            Code = code;
            Reason = reason;
        }
    }
}
