using ASample.NetCore.Domain;
using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Services.Customers.Dtos;

namespace ASample.NetCore.Services.Customers.Queries
{
    public class BrowseCustomers:PagedQueryBase, IQuery<PagedResult<CustomerDto>>
    {

    }
}
