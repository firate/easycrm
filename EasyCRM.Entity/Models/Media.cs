using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyCRM.Entity.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string MediaURL { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool? IsDefault { get; set; }

        [Column("ProductID")]
        public int? ProductId { get; set; }
        public Product Product { get; set; }

        [Column("AccountID")]
        public int? AccountId { get; set; }
        public Account Account { get; set; }

        [Column("ContactID")]
        public int? ContactId { get; set; }
        public Contact Contact { get; set; }

        [Column("BrandID")]
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }

    }
}
