using ASample.NetCore.Authentications.Attributes;

namespace ASample.NetCore.Services.Apis.Framwork
{
    public class AdminAuth : JwtAuthAttribute
    {
        public AdminAuth() : base("admin")
        {
        }
    }
}
