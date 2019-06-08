using ASample.NetCore.Domain;
using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.Services.Products.Dtos;


namespace ASample.NetCore.Services.Products.Queries
{
    public class QueryProducts : PagedQueryBase, IQuery<PagedResult<ProductDto>>
    {
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
    }
}
