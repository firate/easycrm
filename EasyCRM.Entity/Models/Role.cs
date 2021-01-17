﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class Role:IdentityRole<int>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}
