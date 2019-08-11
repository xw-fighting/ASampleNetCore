using ASample.NetCore.Auths.Domains;
using System;
using System.Collections.Generic;

namespace ASample.NetCore.Auths.Models.Rights
{

    public class RightDto
    {
        public Guid Id { get; set; }
        public string RightName { get; set; }
        public string RightIcon { get; set; }
        public string RightUrl { get; set; }
        public List<RightDto> SubRights { get; set; }
    }
    public class RightComparer : IEqualityComparer<TRight>
    {
        public bool Equals(TRight x, TRight y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(TRight obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
