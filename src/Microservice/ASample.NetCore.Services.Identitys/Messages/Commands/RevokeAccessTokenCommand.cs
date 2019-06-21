using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Identitys.Messages.Commands
{
    public class RevokeAccessTokenCommand:ICommand
    {
        public Guid UserId { get; }
        public string Token { get; }

        [JsonConstructor]
        public RevokeAccessTokenCommand(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}
