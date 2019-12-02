using System;

namespace ASample.NetCore.Domain
{
    public interface IEntity:IEntity<Guid>
    {

    }
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
