using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ASample.NetCore.Authentication.Attributes
{
    public class JwtAuthAttribute: AuthAttribute
    {
        public JwtAuthAttribute(string policy = "") : base(JwtBearerDefaults.AuthenticationScheme, policy)
        {
        }
    }
}
