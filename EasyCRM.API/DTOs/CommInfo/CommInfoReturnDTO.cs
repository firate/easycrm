using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.API.DTOs
{
    public class CommInfoReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 

        public int CommunicationTypeId { get; set; }
        public string CommunicationType { get; set; }

        public int? AccountId { get; set; }
        public string Account { get; set; }

        public int? ContactId { get; set; }
        public ContactReturnDTO Contact { get; set; }

        public int? LeadId { get; set; }
        //public string Lead { get; set; }

    }
}
