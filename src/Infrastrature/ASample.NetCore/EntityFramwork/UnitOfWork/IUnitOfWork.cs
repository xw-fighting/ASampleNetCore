using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ASample.NetCore.EntityFramwork
{
    public interface IUnitOfWork<TDbContext> where TDbContext : DbContext , IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();

        TDbContext GetDbContext();
    }

    //public interface IUnitOfWork<TDbContext,TEntity>
    //    where TDbContext : DbContext, IDisposable
    //    where TEntity : class, new()
    //{
    //    DbSet<TEntity> 
    //}
}
