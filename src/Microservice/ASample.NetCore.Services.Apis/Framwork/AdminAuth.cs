using ASample.NetCore.Authentications;

namespace ASample.NetCore.Services.Apis.Framwork
{
    public class AdminAuth : JwtAuthAttribute
    {
        public AdminAuth() : base("admin")
        {
        }
    }
}
