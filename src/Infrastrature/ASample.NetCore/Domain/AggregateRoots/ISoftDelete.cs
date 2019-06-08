using System;

namespace ASample.NetCore.Domain.AggregateRoots
{ 
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }

        DateTime? DeleteTime { get; set; }
    }
}
