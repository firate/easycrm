using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCRM.Business.ModelHelpers
{
    public class CommInfoParams
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public int CommunicationTypeId { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        
    }
}
