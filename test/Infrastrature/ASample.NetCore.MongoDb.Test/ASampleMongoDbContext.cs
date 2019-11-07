using ASample.NetCore.NonRelationalDb;
using MongoDB.Driver;

namespace ASample.NetCore.MongoDb.Test
{
    public class ASampleMongoDbContext : MongoClient
    {

        public ASampleMongoDbContext() { }

        public ASampleMongoDbContext(DbOptions mongoOptions) : base(mongoOptions.ConnectionString)
        {

        }
        public DbOptions DbOptions { get; set; }
    }
}
