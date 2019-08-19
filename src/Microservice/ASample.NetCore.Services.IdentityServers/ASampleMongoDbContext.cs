using ASample.NetCore.NonInertialDb.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ASample.NetCore.Services.IdentityServers
{
    public class ASampleMongoDbContext:MongoClient
    {
        private readonly IOptions<DbOptions> _options;
        public ASampleMongoDbContext() { }
        public ASampleMongoDbContext(IOptions<DbOptions> options):base(options.Value.ConnectionString)
        {

        }

        public DbOptions Options { get; set; }
    }
}
