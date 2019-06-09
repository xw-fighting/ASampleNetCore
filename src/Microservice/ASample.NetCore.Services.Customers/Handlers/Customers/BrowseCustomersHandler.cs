using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Customers.Dtos;
using ASample.NetCore.Services.Customers.Queries;
using ASample.NetCore.Services.Customers.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Handlers.Customers
{
    public class BrowseCustomersHandler : IQueryHandler<BrowseCustomers, PagedResult<CustomerDto>>
    {
        private readonly ICustomersRepository _customerRepository;

        public BrowseCustomersHandler(ICustomersRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<PagedResult<CustomerDto>> HandleAsync(BrowseCustomers query)
        {
            var pagedResult = await _customerRepository.BrowseAsync(query);
            var customers = pagedResult.Items.Select(c => new CustomerDto()
            {
                Id = c.Id,
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address,
                Country = c.Country,
                CreateTime = c.CreateTime,
                Completed = c.Completed,
            });
            return PagedResult<CustomerDto>.From(pagedResult, customers);
        }
    }
}
