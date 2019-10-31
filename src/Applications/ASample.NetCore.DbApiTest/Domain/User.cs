using ASample.NetCore.Domain;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ASample.NetCore.DbApiTest.Domain
{
    public class User:AggregateRoot
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }
}
