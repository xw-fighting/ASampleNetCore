using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Products.Dtos;
using ASample.NetCore.Services.Products.Queries;
using ASample.NetCore.Services.Products.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Products.Handlers
{
    public class QueryProductsHandler : IQueryHandler<QueryProducts, PagedResult<ProductDto>>
    {
        private IProductRepository _productRepository;

        public QueryProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedResult<ProductDto>> HandleAsync(QueryProducts query)
        {
            var pagedResult = await _productRepository.QueryPagedAsync(query);
            var products = pagedResult.Items.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Vendor = p.Vendor,
                Price = p.Price,
                Quantity = p.Quantity
            });
            return PagedResult<ProductDto>.From(pagedResult, products); 
        }
    }
}
