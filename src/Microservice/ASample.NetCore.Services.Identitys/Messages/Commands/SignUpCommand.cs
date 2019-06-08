using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.Services.Identitys.Messages.Commands
{
    public class SignUpCommand:ICommand
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; }
        public string Role { get; }

        [JsonConstructor]
        public SignUpCommand(Guid id, string email, string password, string role)
        {
            Id = id;
            Email = email;
            Password = password;
            Role = role;
        }
    }


}
