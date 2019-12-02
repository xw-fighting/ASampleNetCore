using System;

namespace ASample.NetCore.Domain
{
    public abstract class Entity : Entity<Guid>,IEntity<Guid>//--->为什么要继承IPrimaryKey
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }

    public abstract class Entity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set ; }

        public DateTime CreateTime { get; set; }
    }
}
