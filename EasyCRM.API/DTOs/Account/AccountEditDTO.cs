using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.API.DTOs
{
    public class AccountEditDTO
    {
        public string OrganizationName { get; set; }
        public int AccountTypeId { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
