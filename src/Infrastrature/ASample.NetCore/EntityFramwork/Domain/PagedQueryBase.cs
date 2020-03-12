
namespace ASample.NetCore.EntityFramwork.Domain
{
    public abstract class PagedQueryBase : IPagedQuery
    {
        public int PageSize { get; set; } 
        public int PageNum { get; set; }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }
    }
}
