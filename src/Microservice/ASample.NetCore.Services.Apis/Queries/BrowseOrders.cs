using System;

namespace ASample.NetCore.Services.Apis.Queries
{
    public class BrowseOrders:PagedQuery
    {
        public Guid CustomerId { get; set; }
    }
}
