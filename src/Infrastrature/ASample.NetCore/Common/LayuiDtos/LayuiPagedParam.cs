
using System;

namespace ASample.NetCore.Common
{
    public class LayuiPagedParam
    {
        public int Limit { get; set; }
        public int Page { get; set; }

        public Guid? Id { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
