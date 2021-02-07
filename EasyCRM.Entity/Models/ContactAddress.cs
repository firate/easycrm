using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCRM.Entity.Models
{
    public class ContactAddress
    {
        public int? ContactId { get; set; }
        public Contact Contact { get; set; }

        public int? AddressId { get; set; }
        public Address Address { get; set; }
    }
}
