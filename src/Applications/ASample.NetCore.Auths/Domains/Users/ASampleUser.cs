using Microsoft.AspNetCore.Identity;

namespace ASample.NetCore.Auths.Domains
{
    public class ASampleUser:IdentityUser
    {
        /// <summary>
        /// The users first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The users last name
        /// </summary>
        public string LastName { get; set; }
    }
}
