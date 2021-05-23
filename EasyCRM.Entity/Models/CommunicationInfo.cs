using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class CommunicationInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Column("CommunicationTypeID")]
        public int CommunicationTypeId { get; set; }
        public CommunicationType CommunicationType { get; set; }

        [Column("AccountID")]
        public int? AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty(nameof(Models.Account.CommunicationInfos))]
        public Account Account { get; set; }

        [Column("ContactID")]
        public int? ContactId { get; set; }

        [ForeignKey(nameof(ContactId))]
        [InverseProperty(nameof(Models.Contact.CommunicationInfos))]
        public Contact Contact { get; set; }

        [Column("LeadID")]
        public int? LeadId { get; set; }

        [ForeignKey(nameof(LeadId))]
        [InverseProperty(nameof(Models.Lead.CommunicationInfo))]
        public Lead Lead { get; set; }
    }
}
