using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.API.DTOs
{
    public class AccountEditDTO
    {
        public int AccountId { get; set; }
        public string OrganizationName { get; set; }
        public int AccountTypeId { get; set; }
        public string IdentificationCode { get; set; }
        public string Description { get; set; }
        public int? VatNumber { get; set; }
        public int? AccountGroupId { get; set; }
        public int?  DefaultMediaId { get; set; }
        public bool IsActive { get; set; }
        public List<int> GroupIds { get; set; } = new List<int>();

    }
}
