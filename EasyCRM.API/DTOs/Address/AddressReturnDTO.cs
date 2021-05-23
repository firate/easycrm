using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.API.DTOs
{
    public class AddressReturnDTO
    {

        public int Id { get; set; }
        public string AddressTitle { get; set; }
        public string AddressLine { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int? AccountId { get; set; }
        public string Account { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public bool IsMain { get; set; } 

        //public List<ContactAddress> ContactAddresses { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
