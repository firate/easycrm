using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCRM.Entity.Models
{
    public class AccountGroup
    {
        public virtual Account Account { get; set; }
        public virtual int AccountId { get; set; }
        public virtual Group Group { get; set; }
        public virtual int GroupId { get; set; }
    }
}
