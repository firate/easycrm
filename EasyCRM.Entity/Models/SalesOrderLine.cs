using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class SalesOrderLine
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Quantity { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
