using System;
using System.Collections.Generic;
using System.Text;

namespace ASample.NetCore.NonInertialDb.Options
{
    public class DbOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public bool InMemory { get; set; }
    }
}
