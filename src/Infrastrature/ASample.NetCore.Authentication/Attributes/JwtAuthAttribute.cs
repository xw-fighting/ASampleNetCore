using ASample.NetCore.Authentication.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ASample.NetCore.Authentications.Attributes
{
    public class JwtAuthAttribute: AuthAttribute
    {
        public JwtAuthAttribute(string policy = "") : base(JwtBearerDefaults.AuthenticationScheme, policy)
        {
        }
    }
}
