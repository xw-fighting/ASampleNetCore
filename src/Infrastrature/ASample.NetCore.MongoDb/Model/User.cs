using ASample.NetCore.Domain.Models;
using System;

namespace ASample.NetCore.MongoDb.Model
{
    public class User:AggregateRoot
    {
        //public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }
}
