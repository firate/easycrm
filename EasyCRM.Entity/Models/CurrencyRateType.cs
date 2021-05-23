using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCRM.Entity.Models
{
    public class CurrencyRateType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Currency Currency { get; set; }
    }
}
