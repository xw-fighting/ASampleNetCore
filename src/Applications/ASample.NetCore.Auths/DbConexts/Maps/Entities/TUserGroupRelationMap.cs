using ASample.NetCore.Auths.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASample.NetCore.Auths.DbConexts.Maps
{
    public class TUserGroupRelationMap : IEntityTypeConfiguration<TUserGroupRelation>
    {
        public void Configure(EntityTypeBuilder<TUserGroupRelation> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.UserId);
            builder.Property(c => c.GroupId);
        }
    }
}
