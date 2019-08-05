using ASample.NetCore.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.DbApiTest.Domain
{
    public class UserInfo:AggregateRoot
    {
        [Column("Id",TypeName ="varchar(36)")]
        public override Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Birthday { get; set; }
        public string Tel { get; set; }
    }
}
