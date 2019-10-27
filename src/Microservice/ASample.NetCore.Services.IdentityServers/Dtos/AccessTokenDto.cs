
namespace ASample.NetCore.Services.IdentityServers.Dtos
{
    public class AccessTokenDto
    {

        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string ErrorDescription { get; set; }

        public string TokenType { get; set; }
    }
}
