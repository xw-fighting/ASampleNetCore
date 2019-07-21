﻿using ASample.NetCore.Domain.AggregateRoots;
using System;

namespace ASample.NetCore.Auths.Domains
{
    public class TUserRoleRelation:Entity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
