using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ASample.NetCore.Authentications
{
    public class JwtAuthAttribute : AuthAttribute
    {
        public JwtAuthAttribute(string policy = "") : base(JwtBearerDefaults.AuthenticationScheme, policy)
        {
        }
    }
}
