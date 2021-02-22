using EasyCRM.Entity.Enums;
using EasyCRM.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.ModelHelpers
{
    public class SalesOrderParams:BaseSearchParams
    {
        public int SalesOrderId { get; set; }
        public int AccountId { get; set; }
        public string Status { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
