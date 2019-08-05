using System;

namespace ASample.NetCore.Auths.Models.Rights
{
    public class RightParam
    {
        public Guid Id { get; set; }
        public string RightName { get; set; }
        public Guid ?ParentId { get; set; }
        public string Description { get; set; }
    }
}
