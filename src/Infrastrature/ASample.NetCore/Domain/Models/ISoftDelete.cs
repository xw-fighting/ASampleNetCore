using System;

namespace ASample.NetCore.Domain.Models
{ 
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }

        DateTime? DeleteTime { get; set; }
    }
}
