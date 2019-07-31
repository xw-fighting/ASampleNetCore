using ASample.NetCore.Auths.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASample.NetCore.Auths.DbConexts.Maps
{
    public class TLogMap : IEntityTypeConfiguration<TLog>
    {
        public void Configure(EntityTypeBuilder<TLog> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.OpType);
            builder.Property(c => c.Content).HasMaxLength(50);
            builder.Property(c => c.OpUser).HasMaxLength(500);

            builder.Property(c => c.CreateTime);
            builder.Property(c => c.DeleteTime);
            builder.Property(c => c.ModifyTime);
            builder.Property(c => c.IsDeleted);
        }
    }
}
