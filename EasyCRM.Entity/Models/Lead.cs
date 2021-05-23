using EasyCRM.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class Lead
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public Address DefaultAddress { get; set; }
        [Column("DefaultAddressID")]
        public int DefaultAddressId { get; set; }
        public string AccountName { get; set; }
        public LeadStatus LeadStatus { get; set; }
        public LeadSource LeadSource { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal OpportunityAmount { get; set; }
        public Industry Industry { get; set; }
        public string Description { get; set; }

        public List<CommunicationInfo> CommunicationInfo { get; set; } = new List<CommunicationInfo>();

    }

    

    
}
