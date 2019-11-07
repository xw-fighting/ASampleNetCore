using System;
using System.Threading.Tasks;
using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.NonRelationalDb;
using ASample.NetCore.Services.Customers.Domain;
using ASample.NetCore.Services.Customers.Queries;

namespace ASample.NetCore.Services.Customers.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IRepository<Customer> _repository;

        public CustomersRepository(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<Customer> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<PagedResult<Customer>> BrowseAsync(BrowseCustomers query)
            => await _repository.BrowseAsync(_ => true, query);

        public async Task AddAsync(Customer customer)
            => await _repository.AddAsync(customer);

        public async Task UpdateAsync(Customer customer)
            => await _repository.UpdateAsync(customer);
    }
}
