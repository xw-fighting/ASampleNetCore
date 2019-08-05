using Microsoft.EntityFrameworkCore;
using System;

namespace ASample.NetCore.EntityFramwork
{
    public interface IUnitOfWork<TDbContext> where TDbContext : DbContext , IDisposable
    {
        int SaveChanges();
    }
}
