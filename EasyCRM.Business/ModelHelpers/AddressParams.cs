using EasyCRM.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCRM.Business.ModelHelpers
{
    public class AddressParams:BaseSearchParams
    {
        public int Id { get; set; }
        public string AddressTitle { get; set; }
        public string AddressLine { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }       
        public int? AccountId { get; set; }
        public int CountryId { get; set; }
        public int? ContactId { get; set; }
        public bool? IsMain { get; set; }
    }
}
