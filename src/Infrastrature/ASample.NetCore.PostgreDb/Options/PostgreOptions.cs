
namespace ASample.NetCore.PostgreDb.Options
{
    public class PostgreOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public bool InMemory { get; set; }
    }
}
