using EasyCRM.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.ModelHelpers
{
    public class SalesOrderParams
    {
        public int SalesOrderId { get; set; }
        public int AccountId { get; set; }
        public SalesOrderStatus Status { get; set; }

    }
}
