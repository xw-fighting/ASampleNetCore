using ASample.NetCore.MongoDb.Repository;
using ASample.NetCore.Services.Orders.Domain;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Orders.Repositories
{
    public class CustomersRepository:ICustomersRepository
    {
        private readonly IMongoRepository<Customer> _mongoDbRepository;

        public CustomersRepository(IMongoRepository<Customer> mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        public async Task AddAsync(Customer customer)
        {
            await _mongoDbRepository.AddAsync(customer);
        }

        public async Task<Customer> GetAsync(Guid id)
        {
            var result = await _mongoDbRepository.GetAsync(id);
            return result;
        }
    }
}
