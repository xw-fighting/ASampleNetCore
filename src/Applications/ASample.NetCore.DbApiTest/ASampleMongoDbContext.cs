using ASample.NetCore.NonInertialDb.Options;
using MongoDB.Driver;

namespace ASample.NetCore.DbApiTest
{
    public class ASampleMongoDbContext: MongoClient
    {

        public ASampleMongoDbContext() { }

        public ASampleMongoDbContext(DbOptions mongoOptions) : base(mongoOptions.ConnectionString)
        {
            
        }
        public DbOptions DbOptions { get; set; }
    }
}
