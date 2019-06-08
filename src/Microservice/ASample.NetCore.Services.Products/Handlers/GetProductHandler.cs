using ASample.NetCore.Handlers;
using ASample.NetCore.Services.Products.Dtos;
using ASample.NetCore.Services.Products.Queries;
using ASample.NetCore.Services.Products.Repositories;
using System.Threading.Tasks;

namespace ASample.NetCore.Services.Products.Handlers
{
    public class GetProductHandler : IQueryHandler<GetProduct, ProductDto>
    {
        private IProductRepository _productRepository;

        public GetProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> HandleAsync(GetProduct query)
        {
            var product = await _productRepository.GetAsync(query.Id);

            if (product == null)
                return null;

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Vendor = product.Vendor,
                Price = product.Price
            };
            return productDto;
        }
    }
}
