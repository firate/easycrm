using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyCRM.Entity.Models
{
    public class Tax
    {
        public int Id { get; set; }
        public string TaxCode { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Percent { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
