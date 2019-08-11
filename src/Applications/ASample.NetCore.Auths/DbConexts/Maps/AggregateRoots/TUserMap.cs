using ASample.NetCore.Auths.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASample.NetCore.Auths.DbConexts.Maps
{
    public class TUserMap : IEntityTypeConfiguration<TUser>
    {
        public void Configure(EntityTypeBuilder<TUser> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.LoginName).HasMaxLength(20);
            builder.Property(c => c.UserName).HasMaxLength(20);
            builder.Property(c => c.PhoneNumber).HasMaxLength(12);
            builder.Property(c => c.Email).HasMaxLength(50);

            builder.Property(c => c.CreateTime);
            builder.Property(c => c.DeleteTime);
            builder.Property(c => c.ModifyTime);
            builder.Property(c => c.IsDeleted);
        }
    }
}
