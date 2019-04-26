using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASample.NetCore.ConfigReader
{
    public class ConfigReaderService
    {
        public IConfiguration Configuration { get; set; }
        public ConfigReaderService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GetValue(string key)
        {
            var value = Configuration.GetSection(key);
            return value;
        }
    }
}
