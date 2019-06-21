using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;


namespace ASample.NetCore.Services.Identitys.Messages.Commands
{
    public class RevokeRefreshTokenCommand:ICommand
    {
        public Guid UserId { get; }
        public string Token { get; }

        [JsonConstructor]
        public RevokeRefreshTokenCommand(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}
