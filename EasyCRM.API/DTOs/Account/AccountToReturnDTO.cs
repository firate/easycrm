using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCRM.Entity.Models;

namespace EasyCRM.API.DTOs
{
    public class AccountToReturnDTO
    {
        public int AccountId { get; set; }
        public string OrganizationName { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountType { get; set; }
        public string IdentificationCode { get; set; }
        public string Description { get; set; }
        public int? VatNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ContactReturnDTO> Contacts { get; set; } = new List<ContactReturnDTO>();
        public List<GroupReturnDTO> Groups { get; set; } = new List<GroupReturnDTO>();

    }
}
