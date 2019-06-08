using ASample.NetCore.Domain.AggregateRoots;

namespace ASample.NetCore.WebApi.Domain
{
    public class UserInfo:AggregateRoot
    {
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
