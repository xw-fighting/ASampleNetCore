using ASample.NetCore.SqlServerWebSite.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ASample.NetCore.SqlServerWebSite.EntityMap
{
    public class UserInfoMap : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
