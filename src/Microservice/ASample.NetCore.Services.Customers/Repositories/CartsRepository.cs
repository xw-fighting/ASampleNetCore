﻿using ASample.NetCore.NonRelationalDb;
using ASample.NetCore.Services.Customers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Repositories
{
    public class CartsRepository: ICartsRepository
    {
        private readonly IRepository<Cart> _repository;

        public CartsRepository(IRepository<Cart> repository)
        {
            _repository = repository;
        }

        public async Task<Cart> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<IEnumerable<Cart>> GetAllWithProduct(Guid productId)
            => await _repository.FindAsync(x => x.Items.Any(i => i.ProductId == productId));

        public async Task AddAsync(Cart cart)
            => await _repository.AddAsync(cart);

        public async Task UpdateAsync(Cart cart)
            => await _repository.UpdateAsync(cart);

        public async Task UpdateManyAsync(IEnumerable<Cart> carts)
        {
            foreach (var cart in carts)
            {
                await _repository.UpdateAsync(cart);
            }
        }
    }
}
