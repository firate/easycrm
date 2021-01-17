using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class User:IdentityUser<int>
    {
        public User()
        {
            UserRoles = new List<UserRole>();
        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [Column("UserTypeID")]
        public int UserTypeId { get; set; }
        
        [ForeignKey(nameof(UserTypeId))]
        public UserType UserType { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}

