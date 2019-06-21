using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;

namespace ASample.NetCore.Services.Identitys.Messages.Commands
{
    public class RefreshAccessTokenCommand:ICommand
    {
        public string Token { get; }

        [JsonConstructor]
        public RefreshAccessTokenCommand(string token)
        {
            Token = token;
        }
    }
}
