﻿using System;

namespace ASample.NetCore.Domain
{
    public abstract class AggregateRoot: AggregateRoot<Guid>,IAggregateRoot
    {
        public override Guid Id { get; set; } = Guid.NewGuid();

        protected virtual void SetUpdatedDate()
            => ModifyTime = DateTime.Now;

        protected virtual void SetDeleteDate()
            => DeleteTime = DateTime.Now;
    }
        
    public abstract class AggregateRoot<TKey> : IPrimaryKey<TKey>, IAggregateRoot<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual TKey Id { get; set ; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>

        public virtual DateTime? DeleteTime { get ; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? ModifyTime { get; set; }
    }
}
