
using System.Threading.Tasks;

namespace ASample.NetCore.MongoDb
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync();
    }
}
