
namespace ASample.NetCore.RelationalDb.Optiions
{
    public class DbOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public bool InMemory { get; set; }
    }
}
