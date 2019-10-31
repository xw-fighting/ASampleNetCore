using ASample.NetCore.Domain;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ASample.NetCore.NonInertialDb
{
    public class MongoDbAggregateRoot:AggregateRoot
    {
        [BsonId]
        public override Guid Id { get; set; }
    }
}
