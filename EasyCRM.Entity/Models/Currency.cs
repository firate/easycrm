using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyCRM.Entity.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal ConversionFactor { get; set; }
        public int AmountDecimals { get; set; }
        public DateTime ValidFromDate { get; set; }
        public DateTime ValidUntilDate { get; set; }
    }
}
