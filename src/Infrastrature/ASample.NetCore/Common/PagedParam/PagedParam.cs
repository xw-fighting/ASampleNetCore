using System;

namespace ASample.NetCore.Common
{
    public class PagedParam
    {
        public int PageSize { get; set; }
        public int PageNum { get; set; }

        public Guid? Id { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
