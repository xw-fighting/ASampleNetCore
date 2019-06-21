using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Identitys.Messages.Events
{
    public class RefreshTokenRevokedEvent:IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public RefreshTokenRevokedEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}
