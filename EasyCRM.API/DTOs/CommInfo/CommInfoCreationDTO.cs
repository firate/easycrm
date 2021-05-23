using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.API.DTOs
{
    public class CommInfoCreationDTO
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public int CommunicationTypeId { get; set; }
        public int? AccountId { get; set; }
        public int? ContactId { get; set; }
        public int? LeadId { get; set; }

    }
}
