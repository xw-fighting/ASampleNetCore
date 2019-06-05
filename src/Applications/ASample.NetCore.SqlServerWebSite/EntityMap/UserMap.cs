using ASample.NetCore.SqlServerWebSite.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASample.NetCore.SqlServerWebSite.EntityMap
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Address).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Age).IsRequired();

            builder.Property(p => p.CreateTime).IsRequired();
            builder.Property(p => p.IsDeleted).IsRequired();
        }
    }
}
