using ASample.NetCore.MySqlDb.Options;
using ASample.NetCore.SqlServerWebSite.EntityMap;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ASample.NetCore.SqlServerWebSite
{
    public class ASampleMySqlDbContext:DbContext
    {
        private readonly IOptions<MySqlOptions> _mySqlOptions;
        public ASampleMySqlDbContext(IOptions<MySqlOptions> mySqlOptions) :base()
        {
            _mySqlOptions = mySqlOptions;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            if (_mySqlOptions.Value.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase(_mySqlOptions.Value.Database);
                return;
            }
            optionsBuilder.UseMySql(_mySqlOptions.Value.ConnectionString);
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
