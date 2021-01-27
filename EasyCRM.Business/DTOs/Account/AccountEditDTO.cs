using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.DTOs
{
    public class AccountEditDTO
    {
        public string OrganizationName { get; set; }
        public int AccountTypeId { get; set; }
        public string Description { get; set; }
    }
}
