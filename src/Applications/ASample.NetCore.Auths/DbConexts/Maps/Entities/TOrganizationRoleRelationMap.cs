using ASample.NetCore.Auths.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASample.NetCore.Auths.DbConexts.Maps
{
    public class TOrganizationRoleRelationMap : IEntityTypeConfiguration<TOrganizationRoleRelation>
    {
        public void Configure(EntityTypeBuilder<TOrganizationRoleRelation> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.OrgId);
            builder.Property(c => c.RoleId);
        }
    }
}
