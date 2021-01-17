using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class UserType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [InverseProperty(nameof(Models.User.UserType))]
        public List<User> User { get; set; }
    }
}
