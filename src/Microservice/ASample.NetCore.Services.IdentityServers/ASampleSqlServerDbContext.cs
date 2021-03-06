﻿using Microsoft.EntityFrameworkCore;
using ASample.NetCore.RelationalDb;
using Microsoft.Extensions.Options;

namespace ASample.NetCore.Services.IdentityServers
{
    public class ASampleSqlServerDbContext : DbContext
    {
        private readonly IOptions<DbOptions> _sqlOptions;

        public ASampleSqlServerDbContext(IOptions<DbOptions> sqlOptions) : base()
        {
            _sqlOptions = sqlOptions;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            optionsBuilder.UseSqlServer(_sqlOptions.Value.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new UserMap());
            //modelBuilder.ApplyConfiguration(new UserInfoMap());
            //modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductsReportConfiguration(_jsonSerializer));
        }
    }
}
