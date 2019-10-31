using System;

namespace ASample.NetCore.Domain
{ 
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }

        DateTime? DeleteTime { get; set; }
    }
}
