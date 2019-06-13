using ASample.NetCore.Services.Orders.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Orders.Repositories
{
    public interface ICustomersRepository
    {
        Task<Customer> GetAsync(Guid id);
        Task AddAsync(Customer customer);
    }
}
