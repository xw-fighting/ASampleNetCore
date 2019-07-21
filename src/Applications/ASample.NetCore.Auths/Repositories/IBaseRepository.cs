using ASample.NetCore.EntityFramwork.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASample.NetCore.Auths.Repositories
{
    public interface IBaseRepository<T> where T:class ,new()
    {
        Task<List<T>> QueryAsync(Expression<Func<T, bool>> predicate = null);
        Task<PagedResult<T>> QueryPagedAsync<s>(int page, int limit
            , Expression<Func<T, s>> sortLamda
            , Expression<Func<T, bool>> whereLamda
            , bool isAsc = false);
        Task AddAsync(T param);
        Task UpdateAsync(T param);
        Task DeleteAsync(T param);
    }
}
