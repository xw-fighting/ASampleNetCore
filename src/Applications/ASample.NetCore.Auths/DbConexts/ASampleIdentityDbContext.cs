using ASample.NetCore.Auths.DbConexts.Maps;
using ASample.NetCore.Auths.Domains;
using ASample.NetCore.SqlServerDb.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ASample.NetCore.Auths.DbConexts
{
    public class ASampleIdentityDbContext: IdentityDbContext<ASampleUser>
    {
        private readonly IOptions<SqlServerOptions> _sqlOptions;

        public ASampleIdentityDbContext(IOptions<SqlServerOptions> sqlOptions) : base()
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
            modelBuilder.ApplyConfiguration(new TUserMap());
            modelBuilder.ApplyConfiguration(new TOrganizationMap());
            modelBuilder.ApplyConfiguration(new TRoleMap());
            modelBuilder.ApplyConfiguration(new TRightMap());
            modelBuilder.ApplyConfiguration(new TGroupMap());
            modelBuilder.ApplyConfiguration(new TLogMap());
            modelBuilder.ApplyConfiguration(new TOrganizationRoleRelationMap());
            modelBuilder.ApplyConfiguration(new TUserRoleRelationMap());
            modelBuilder.ApplyConfiguration(new TUserGroupRelationMap());
            modelBuilder.ApplyConfiguration(new TGroupRoleRelationMap());
            modelBuilder.ApplyConfiguration(new TRoleRightRelationMap());
            
        }

        ///// <summary>
        ///// Default constructor, expecting database options passed in
        ///// </summary>
        ///// <param name="options">The database context options</param>
        //public ASampleIdentityDbContext(DbContextOptions<ASampleIdentityDbContext> options) : base(options)
        //{

        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Fluent API

        //    //modelBuilder.Entity<SettingsDataModel>().HasIndex(a => a.Name);
        //}
    }
}
