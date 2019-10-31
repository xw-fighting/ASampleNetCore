using System;

namespace ASample.NetCore.Domain
{ 
    public interface IPrimaryKey<T>
    {
        T Id { get; set; }
    }

    public interface IPrimaryKey : IPrimaryKey<Guid>
    {

    }
}
