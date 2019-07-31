using ASample.NetCore.Auths.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASample.NetCore.Auths.DbConexts.Maps
{
    public class TGroupRoleRelationMap : IEntityTypeConfiguration<TGroupRoleRelation>
    {
        public void Configure(EntityTypeBuilder<TGroupRoleRelation> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.GroupId);
            builder.Property(c => c.RoleId);
        }
    }
}
