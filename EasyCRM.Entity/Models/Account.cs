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
        }
        [Key]
        [Column("AccountID")]
        public int AccountId { get; set; }

        public string OrganizationName { get; set; }

        [Column("AccountTypeID")]
        public int AccountTypeId { get; set; }

        [ForeignKey(nameof(AccountTypeId))]
        public AccountType AccountType { get; set; }
        
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<CommunicationInfo> CommunicationInfos { get; set; }
        public List<Industry> Industries { get; set; } 
        public List<Address> Addresses { get; set; } 
        public List<SalesOrder> SalesOrders { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
