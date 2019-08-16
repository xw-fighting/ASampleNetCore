using ASample.NetCore.DbApiTest.EntityMap;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ASample.NetCore.RelationalDb.Options;

namespace ASample.NetCore.DbApiTest
{
    public class ASampleSqlServerDbContext:DbContext
    {
        private readonly IOptions<DbOptions> _sqlOptions;

        public ASampleSqlServerDbContext(IOptions<DbOptions> sqlOptions):base()
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
