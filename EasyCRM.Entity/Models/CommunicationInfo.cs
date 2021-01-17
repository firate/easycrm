using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class CommunicationInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CommunicationType CommunicationType { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        

    }
}
