using EasyCRM.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class SalesOrder
    {
        public SalesOrder()
        {
            OrderLines= new List<SalesOrderLine>();
        }

        [Key]
        [Column("SalesOrderID")]
        public int SalesOrderId { get; set; }

        public int RevisionNumber { get; set; }

        [Column("AccountID")]
        public int AccountId { get; set; }
        
        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
        
        public SalesOrderStatus Status { get; set; }

        public List<SalesOrderLine> OrderLines { get; set; }

        [Column("BillToAddressID")]
        public int BillToAddressId { get; set; }

        [ForeignKey(nameof(BillToAddressId))]
        [InverseProperty(nameof(Address.SalesOrderBillToAddress))]
        public Address BillToAddress { get; set; }

        [Column("ShipToAddressID")]
        public int ShipToAddressId { get; set; }

        [ForeignKey(nameof(ShipToAddressId))]
        [InverseProperty(nameof(Address.SalesOrderShipToAddress))]
        public Address ShipToAddress { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime UpdatedAt { get; set; }

    }

    
}
