
namespace ASample.NetCore.Auths.Models.Account
{
    public class UpdatePasswordModel
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
