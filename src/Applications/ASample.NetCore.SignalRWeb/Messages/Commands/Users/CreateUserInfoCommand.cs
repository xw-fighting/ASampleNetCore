using ASample.NetCore.Domain.Message;
using System;

namespace ASample.NetCore.SignalRWeb.Messages.Commands.Users
{
    public class CreateUserInfoCommand:ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }

        public CreateUserInfoCommand(string name, string address, int age)
        {
            Name = name;
            Address = address;
            Age = age;
        }
    }
}
