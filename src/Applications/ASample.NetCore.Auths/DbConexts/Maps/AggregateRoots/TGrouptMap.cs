using ASample.NetCore.Auths.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASample.NetCore.Auths.DbConexts.Maps
{
    public class TGroupMap : IEntityTypeConfiguration<TGroup>
    {
        public void Configure(EntityTypeBuilder<TGroup> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.ParentId);
            builder.Property(c => c.GroupName).HasMaxLength(50);
            builder.Property(c => c.Description).HasMaxLength(500);

            builder.Property(c => c.CreateTime);
            builder.Property(c => c.DeleteTime);
            builder.Property(c => c.ModifyTime);
            builder.Property(c => c.IsDeleted);
        }
    }
}
