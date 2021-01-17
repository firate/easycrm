using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class OpportunityContact
    {
        public int OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }

        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
