using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceMVC.Models
{
    public class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
