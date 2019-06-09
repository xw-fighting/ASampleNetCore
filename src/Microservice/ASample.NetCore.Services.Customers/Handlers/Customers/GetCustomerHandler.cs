using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Customers.Dtos;
using ASample.NetCore.Services.Customers.Queries;
using ASample.NetCore.Services.Customers.Repositories;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Handlers.Customers
{
    public class GetCustomerHandler : IQueryHandler<GetCustomer, CustomerDto>
    {
        private readonly ICustomersRepository _customerRepository;

        public GetCustomerHandler(ICustomersRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> HandleAsync(GetCustomer query)
        {
            var customer = await _customerRepository.GetAsync(query.Id);
            return customer == null?null:new CustomerDto
            {
                Id = customer.Id,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Country = customer.Country,
                CreateTime = customer.CreateTime
            };
        }
    }
}
