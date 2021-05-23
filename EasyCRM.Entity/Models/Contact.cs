using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EasyCRM.Entity.Models
{
    public class Contact
    {
        public Contact()
        {
            CommunicationInfos = new List<CommunicationInfo>();
            OpportunityContacts = new List<OpportunityContact>();
            ContactAddresses = new List<ContactAddress>();
        }
        
        [Key]
        [Column("ContactID")]
        public int ContactId { get; set; }
        
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NameTitle { get; set; }
        
        public List<CommunicationInfo> CommunicationInfos { get; set; }
        
        [Column("SelectedAddressID")]
        public int? SelectedAddressId { get; set; }
        
        [ForeignKey(nameof(SelectedAddressId))]
        public Address SelectedAddress { get; set; }

        public List<ContactAddress> ContactAddresses { get; set; }

        [Column("AccountID")]
        public int? AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; }

        public bool? IsPrincipalContact { get; set; }

        public List<OpportunityContact> OpportunityContacts { get; set; }

        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
