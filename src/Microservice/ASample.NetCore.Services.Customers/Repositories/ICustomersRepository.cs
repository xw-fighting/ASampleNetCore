using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Services.Customers.Domain;
using ASample.NetCore.Services.Customers.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Repositories
{
    public interface ICustomersRepository
    {
        Task UpdateAsync(Customer customer);

        Task<Customer> GetAsync(Guid id);

        Task<PagedResult<Customer>> BrowseAsync(BrowseCustomers query);

        Task AddAsync(Customer customer);

    }
}
