using ASample.NetCore.Auths.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASample.NetCore.Auths.DbConexts.Maps
{
    public class TRightMap : IEntityTypeConfiguration<TRight>
    {
        public void Configure(EntityTypeBuilder<TRight> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.ParentId);
            builder.Property(c => c.RightName).HasMaxLength(50);
            builder.Property(c => c.Description).HasMaxLength(500);

            builder.Property(c => c.CreateTime);
            builder.Property(c => c.DeleteTime);
            builder.Property(c => c.ModifyTime);
            builder.Property(c => c.IsDeleted);
        }
    }
}
