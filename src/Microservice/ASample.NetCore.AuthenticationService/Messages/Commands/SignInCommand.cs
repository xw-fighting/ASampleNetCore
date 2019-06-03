using Newtonsoft.Json;

namespace ASample.NetCore.AuthenticationService.Messages.Commands
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
