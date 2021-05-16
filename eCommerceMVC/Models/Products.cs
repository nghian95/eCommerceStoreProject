using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eCommerceMVC.Models
{
    public partial class Products
    {
        public Products()
        {
            Transactions = new HashSet<Transactions>();
        }

        public string Sku { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a quantity")]
        [RegularExpression("[1-9][0-9]*", ErrorMessage ="Quantity must be more than 0")]
        public int? Quantity { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
        public double? Sale { get; set; }

        public Categories Category { get; set; }
        public ICollection<Transactions> Transactions { get; set; }
    }
}
