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
        public string AccountType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ContactReturnDTO> Contacts { get; set; }
    }
}
