using EasyCRM.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.ModelHelpers
{
    public class ProductParams: BaseSearchParams
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public string UPC { get; set; }
        public string SKU { get; set; }
        public string EAN { get; set; }
        public string Volume { get; set; }
        public string Size { get; set; }
        public int? VendorId { get; set; }

    }
}
