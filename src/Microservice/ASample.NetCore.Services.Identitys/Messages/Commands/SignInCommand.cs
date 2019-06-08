using Newtonsoft.Json;

namespace ASample.NetCore.Services.Identitys.Messages.Commands
{
    public class SignInCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonConstructor]
        public SignInCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
