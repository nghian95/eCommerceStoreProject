using System;
using System.Collections.Generic;

namespace eCommerceClassLibrary.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
