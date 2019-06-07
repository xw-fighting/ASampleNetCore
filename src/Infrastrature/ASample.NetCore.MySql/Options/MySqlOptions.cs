
namespace ASample.NetCore.MySqlDb.Options
{
    public class MySqlOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public bool InMemory { get; set; }
    }
}
