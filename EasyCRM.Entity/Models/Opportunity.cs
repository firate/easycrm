using EasyCRM.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class Opportunity
    {
        public Opportunity()
        {
            OpportunityContacts = new List<OpportunityContact>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Account Account { get; set; }
        public OpportunityStage OpportunityStage { get; set; }
        [Column(TypeName ="decimal(10,2)")]
        public decimal? Amount { get; set; }
        public int? Probability { get; set; }
        public LeadSource LeadSource { get; set; }
        public DateTime CloseDate { get; set; }
        public List<OpportunityContact> OpportunityContacts { get; set; }
    }

    

}
