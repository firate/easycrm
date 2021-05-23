using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCRM.Entity.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<AccountGroup> AccountGroups { get; set; }
    }
}
