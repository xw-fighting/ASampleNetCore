using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Customers.Dtos;
using ASample.NetCore.Services.Customers.Queries;
using ASample.NetCore.Services.Customers.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Customers.Handlers.Customers
{
    public class GetCartHandler : IQueryHandler<GetCart, CartDto>
    {
        private readonly ICartsRepository _cartsRepository;

        public GetCartHandler(ICartsRepository cartsRepository)
        {
            _cartsRepository = cartsRepository;
        }

        public async Task<CartDto> HandleAsync(GetCart query)
        {
            var cart = await _cartsRepository.GetAsync(query.Id);
            return cart == null ? null : new CartDto()
            {
                Id = cart.Id,
                Items = cart.Items.Select(x => new CartItemDto
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                })
            };
        }
    }
}
