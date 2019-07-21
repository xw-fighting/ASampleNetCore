using ASample.NetCore.Auths.Domains.Values;
using ASample.NetCore.Domain.AggregateRoots;

namespace ASample.NetCore.Auths.Domains
{
    public class TLog:AggregateRoot
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public OpType OpType { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OpUser { get; set; }
    }
}
