using EasyCRM.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.API.DTOs
{
    public class AccountCreationDTO
    {
        public string OrganizationName { get; set; }
        public int AccountTypeId { get; set; }
        public string IdentificationCode { get; set; }
        public int? VatNumber { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }        
    }
}
