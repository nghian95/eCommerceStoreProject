using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceMVC.Models
{
    public class Products /*: IEquatable<Products>*/
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
        public double? Sale { get; set; }
        public Categories Category { get; set; }

        //public bool Equals(Products product)
        //{
        //    if (product == null)
        //    {
        //        return false;
        //    }
        //    return (this.SKU.Equals(product.SKU) && this.Name.Equals(product.Name) && this.Quantity.Equals(product.Quantity) &&
        //        this.Description.Equals(product.Description) && this.Price.Equals(product.Price) && this.CategoryId.Equals(product.CategoryId) &&
        //        this.Sale.Equals(product.Sale) && this.Category.Equals(product.Category));
        //}
    }
}
