using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceMVC.Models
{
    public class Products
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public int? CategoryId { get; set; }
        public double? Sale { get; set; }
        public Categories Category { get; set; }
    }
}
