using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class Account
    {
        public Account()
        {
            CommunicationInfos = new List<CommunicationInfo>();
            Industries= new List<Industry>();
            Addresses= new List<Address>();
            SalesOrders = new List<SalesOrder>();
            Contacts = new List<Contact>();
            AccountGroups = new List<AccountGroup>();
            Photos = new List<Media>();
        }
        [Key]
        [Column("AccountID")]
        public int AccountId { get; set; }

        [Required]
        public string OrganizationName { get; set; }
        public string IdentificationCode { get; set; }
        public int? VatNumber { get; set; }
        public bool IsActive { get; set; }

        [Column("AccountTypeID")]
        public int AccountTypeId { get; set; }

        [ForeignKey(nameof(AccountTypeId))]
        public AccountType AccountType { get; set; }

        public string Description { get; set; }
        public List<Media> Photos { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<CommunicationInfo> CommunicationInfos { get; set; }
        public List<Industry> Industries { get; set; } 
        public List<Address> Addresses { get; set; } 
        public List<SalesOrder> SalesOrders { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<AccountGroup> AccountGroups { get; set; }

    }
}
