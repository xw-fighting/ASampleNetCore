using ASample.NetCore.Domain.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.WebApi.Messages.Command
{
    public class UserInfoCreateCommand:ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }

        //public UserInfo(string name, string address, int age)
        //{
        //    Name = name;
        //    Address = address;
        //    Age = age;
        //}
    }
}
