using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.API.DTOs
{
    public class ContactReturnDTO
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NameTitle { get; set; }
        public int SelectedAddressId { get; set; }
        public int AccountId { get; set; }
        public string Notes { get; set; }
    }
}
