
using ASample.NetCore.Domain;

namespace ASample.NetCore.EntityFramwork.Domain
{
    public interface IPagedQuery : IQuery
    {
        int PageSize { get; }
        int PageNum { get; }
        string OrderBy { get; }
        string SortOrder { get; }
    }
}
