using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;

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

        [JsonConverter(typeof(ChinaDateTimeConverter))]
        public DateTime CreateTime { get; set; }
    }
}
