using ASample.NetCore.RelationalDb.Options; 
using ASample.NetCore.RelationDb.Test.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ASample.NetCore.RelationDb.Test
{
    public class ASamplePostgreDbContext : DbContext
    {
        private readonly IOptions<DbOptions> _postgreOptions;
        public ASamplePostgreDbContext(IOptions<DbOptions> postgrelOptions) : base()
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
            //modelBuilder.ApplyConfiguration(new UserInfoMap());
            //modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductsReportConfiguration(_jsonSerializer));
        }

    }
}
