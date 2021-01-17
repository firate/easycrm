using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CleanURL { get; set; } // slug
        public string Description { get; set; }
        public string Note { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? UnitPrice { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? UnitsInStock{ get; set; }

        [Column("CategoryID")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }




    }
}
