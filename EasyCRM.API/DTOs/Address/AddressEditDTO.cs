using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.API.DTOs
{
    public class AddressEditDTO
    {
        public string AddressTitle { get; set; }
        public string AddressLine { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int AccountId { get; set; }
        public int CountryId { get; set; }
        public int ContactId { get; set; }
        public bool IsMain { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

   
}
