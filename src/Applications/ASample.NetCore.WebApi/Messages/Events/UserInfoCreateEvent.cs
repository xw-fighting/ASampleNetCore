using ASample.NetCore.Domain.Message;
using Newtonsoft.Json;
using System;

namespace ASample.NetCore.WebApi.Messages.Events
{
    [MessageNamespace("userinfo")]
    public class UserInfoCreateEvent:IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }

        [JsonConstructor]
        public UserInfoCreateEvent(Guid id,string name ,string address,int age)
        {
            Id = id;
            Name = name;
            Address = address;
            Age = age;
        }
    }
}
