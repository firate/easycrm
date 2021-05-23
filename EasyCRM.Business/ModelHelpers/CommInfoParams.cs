using EasyCRM.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCRM.Business.ModelHelpers
{
    public class CommInfoParams: BaseSearchParams
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public int? ContactId { get; set; }
        public int? LeadId { get; set; }
        public string Name { get; set; }
        public int CommunicationTypeId { get; set; }
        public string Value { get; set; }
        
    }
}
