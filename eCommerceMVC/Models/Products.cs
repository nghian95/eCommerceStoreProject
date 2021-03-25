using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
//using System.Web.Mvc;

namespace eCommerceMVC.Models
{
    public class Products /*: IEquatable<Products>*/
    {
        [Required]
        public string Sku { get; set; }
        [Required]
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
