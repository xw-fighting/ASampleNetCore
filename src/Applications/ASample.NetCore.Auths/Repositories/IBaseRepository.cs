using ASample.NetCore.EntityFramwork.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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

        Task<PagedResult<T>> QueryPagedAsync(int page, int limit
           , Func<IQueryable<T>, IQueryable<T>> whereLamda = null
           , bool isAsc = true);
        Task AddAsync(T param);
        Task UpdateAsync(T param);
        Task DeleteAsync(T param);
        Task DeleteAsync(Guid id);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate = null);
    }
}
