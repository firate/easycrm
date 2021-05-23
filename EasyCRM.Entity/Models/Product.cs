using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class Product
    {
        public Product()
        {
            Photos = new List<Media>();
        }

        public int Id { get; set; }
        [Column("Upc")]
        public string UPC { get; set; }
        [Column("Sku")]
        public string SKU { get; set; }
        [Column("Ean")]
        public string EAN { get; set; }
        public string Name { get; set; }
        public string CleanURL { get; set; } // ex: /billy-bookcase-white
        public string Description { get; set; }
        public string Note { get; set; }
        
        [Column(TypeName = "decimal(10,4)")]
        public decimal? UnitPrice { get; set; }
        
        [Column(TypeName="decimal(10,4)")]
        public decimal? CostPrice { get; set; }

        [Column(TypeName = "decimal(10,4)")]
        public decimal? UnitsInStock{ get; set; }

        public string Volume { get; set; }
        public string Size { get; set; }

        public List<Media> Photos { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Column("BrandID")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [Column("CurrencyID")]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        [Column("UnitCodeID")]
        public int UnitCodeId { get; set; }
        public UnitCode UnitCode { get; set; }

        [Column("CategoryID")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Column("VendorID")]
        public int? VendorId { get; set; }
        public Account Vendor { get; set; }
    }
}
