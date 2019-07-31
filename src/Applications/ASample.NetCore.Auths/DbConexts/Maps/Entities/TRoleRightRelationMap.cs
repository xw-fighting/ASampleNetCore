using ASample.NetCore.Auths.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASample.NetCore.Auths.DbConexts.Maps
{
    public class TRoleRightRelationMap : IEntityTypeConfiguration<TRoleRightRelation>
    {
        public void Configure(EntityTypeBuilder<TRoleRightRelation> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.RightId);
            builder.Property(c => c.RoleId);
            builder.Property(c => c.RightType);
        }
    }
}
