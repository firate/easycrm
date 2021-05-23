using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyCRM.Entity.Models
{
    public class CurrencyRate
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal Rate { get; set; }
        public DateTime ValidFrom { get; set; }
        public Currency Currency { get; set; }


    }
}
