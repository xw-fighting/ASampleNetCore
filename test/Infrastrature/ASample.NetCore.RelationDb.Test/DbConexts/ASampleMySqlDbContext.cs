﻿using ASample.NetCore.RelationalDb;
using ASample.NetCore.RelationDb.Test.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace ASample.NetCore.RelationDb.Test
{
    public class ASampleMySqlDbContext : DbContext
    {
        private readonly IOptions<DbOptions> _mySqlOptions;
        public ASampleMySqlDbContext(IOptions<DbOptions> mySqlOptions) : base()
        {
            _mySqlOptions = mySqlOptions;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            //if (_mySqlOptions.Value.InMemory)
            //{
            //    optionsBuilder.UseInMemoryDatabase(_mySqlOptions.Value.Database);
            //    return;
            //}
            optionsBuilder.UseMySql(_mySqlOptions.Value.ConnectionString,
                mysqlOptions =>
                {
                    mysqlOptions.ServerVersion(new Version(5, 1, 62), ServerType.MySql);
                });
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
