using ASample.NetCore.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ASample.NetCore.MongoDb.Test.Model
{
    public class User:AggregateRoot
    {
        //public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }
}
