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
        
        [Column(TypeName = "decimal(10,4)")]
        public double Quantity { get; set; }

        // current unit price for sales order
        [Column(TypeName = "decimal(10,4)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal DiscountRate { get; set; }

        [Column(TypeName = "decimal(10,4)")]
        public decimal DiscountAmount { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        [Column("SalesOrderID")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

        [Column("ProductID")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
