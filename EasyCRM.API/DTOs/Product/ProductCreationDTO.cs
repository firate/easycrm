﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.API.DTOs
{
    public class ProductCreationDTO
    {
        public string UPC { get; set; }
        public string SKU { get; set; }
        public string EAN { get; set; }
        public string Name { get; set; }
        public string CleanURL { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public string Note { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? CostPrice { get; set; }
        public int UnitCodeId { get; set; }
        public decimal? UnitsInStock { get; set; }
        public string Volume { get; set; }
        public string Size { get; set; }
        public int CurrencyId { get; set; }
        public int CategoryId { get; set; }
        public int? VendorId { get; set; }

    }
}
