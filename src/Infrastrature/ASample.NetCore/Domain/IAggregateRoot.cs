using System;

namespace ASample.NetCore.Domain
{
    public interface IAggregateRoot:IAggregateRoot<Guid>
    {

    }

    public interface IAggregateRoot<TKey>: ISoftDelete
    {
        TKey Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime? ModifyTime { get; set; }
    }
}
