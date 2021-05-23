using EasyCRM.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.ModelHelpers
{
    public class LeadParams: BaseSearchParams
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }

        public int IndustryId { get; set; }
        public int AccountId { get; set; }
        

    }
}
