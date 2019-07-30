using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ASample.NetCore.Common;
using ASample.NetCore.Domain.AggregateRoots;
using ASample.NetCore.EntityFramwork;
using ASample.NetCore.EntityFramwork.Domain;
using ASample.NetCore.SqlServerDb.Repository;

namespace ASample.NetCore.Auths.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : AggregateRoot, new()
    {
        private ISqlServerRepository<T> _sqlServerRepository;
        private IUnitOfWork _unitOfWork;
        public BaseRepository(
            ISqlServerRepository<T> sqlServerRepository
            , IUnitOfWork unitOfWork
            )
        {
            _sqlServerRepository = sqlServerRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(T param)
        {
            await _sqlServerRepository.AddAsync(param);
        }

        public async Task DeleteAsync(T param)
        {
            await _sqlServerRepository.DeleteAsync(param.Id);
        }

        public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> result;
            if(predicate == null)
                result = await _sqlServerRepository.QueryAsync(c => true);
            result = await _sqlServerRepository.QueryAsync(predicate);
            return result.ToList();
        }

        public async Task<PagedResult<T>> QueryPagedAsync<s>(int page,int limit
            , Expression<Func<T, s>> sortLamda
            , Expression<Func<T,bool>> whereLamda
            ,bool isAsc = false)
        {
            var result = await _sqlServerRepository.QueryPagedAsync(page,limit,sortLamda,whereLamda,isAsc);
            return result;
        }

        public async Task<PagedResult<T>> QueryPagedAsync(int page, int limit
        , Func<IQueryable<T>, IQueryable<T>> whereLamda = null
        , bool isAsc = false)
        {
            var result = await _sqlServerRepository.QueryPagedAsync(page, limit, c=>c.CreateTime, whereLamda, isAsc);
            return result;
        }

        public async Task UpdateAsync(T param)
        {
            await _sqlServerRepository.UpdateAsync(param);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate = null)
        {
            T result = new T();
            if (predicate == null)
                result = await _sqlServerRepository.GetAsync(c => true);
            result = await _sqlServerRepository.GetAsync(predicate);
            return result;
        }
    }
}
