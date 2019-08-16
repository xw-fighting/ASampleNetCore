using ASample.NetCore.DbApiTest.EntityMap;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ASample.NetCore.RelationalDb.Options;

namespace ASample.NetCore.DbApiTest
{
    public class AsamplePostgreDbContext: DbContext
    {
        private readonly IOptions<DbOptions> _postgreOptions;
        public AsamplePostgreDbContext(IOptions<DbOptions> postgrelOptions) : base()
        {
            _postgreOptions = postgrelOptions;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }
            optionsBuilder.UseNpgsql(_postgreOptions.Value.ConnectionString);
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
