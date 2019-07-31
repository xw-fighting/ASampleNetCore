using ASample.NetCore.Auths.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASample.NetCore.Auths.DbConexts.Maps
{
    public class TUserRoleRelationMap : IEntityTypeConfiguration<TUserRoleRelation>
    {
        public void Configure(EntityTypeBuilder<TUserRoleRelation> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.UserId);
            builder.Property(c => c.RoleId);
        }
    }
}
