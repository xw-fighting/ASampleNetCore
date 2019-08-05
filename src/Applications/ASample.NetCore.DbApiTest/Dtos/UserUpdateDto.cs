using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.DbApiTest.Dtos
{
    public class UserUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
