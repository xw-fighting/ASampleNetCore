
namespace ASample.NetCore.SqlServer.Options
{
    public class SqlServerOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public bool InMemory { get; set; }
    }
}
