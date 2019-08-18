using ASample.NetCore.Domain.AggregateRoots;
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
