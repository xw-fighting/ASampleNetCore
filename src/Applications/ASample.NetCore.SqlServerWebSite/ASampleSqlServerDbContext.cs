using ASample.NetCore.SqlServerDb.Options;
using ASample.NetCore.SqlServerWebSite.EntityMap;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ASample.NetCore.SqlServerWebSite
{
    public class ASampleSqlServerDbContext:DbContext
    {
        private readonly IOptions<SqlServerOptions> _sqlOptions;

        public ASampleSqlServerDbContext(IOptions<SqlServerOptions> sqlOptions):base()
        {
            _sqlOptions = sqlOptions;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            if (_sqlOptions.Value.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase(_sqlOptions.Value.Database);

                return;
            }

            optionsBuilder.UseSqlServer(_sqlOptions.Value.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserInfoMap());
            //modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductsReportConfiguration(_jsonSerializer));
        }
    }
}
