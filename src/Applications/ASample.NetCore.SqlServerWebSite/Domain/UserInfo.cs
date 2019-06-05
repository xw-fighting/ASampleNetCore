using ASample.NetCore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.SqlServerWebSite.Domain
{
    public class UserInfo:AggregateRoot
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Birthday { get; set; }
        public string Tel { get; set; }
    }
}
