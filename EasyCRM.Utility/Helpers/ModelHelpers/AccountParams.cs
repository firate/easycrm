using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Utility
{
    public class AccountParams: BaseSearchParams
    {
        public int AccountId { get; set; }
        public string OrganizationName { get; set; }
        public string AccountType { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

    }
}
