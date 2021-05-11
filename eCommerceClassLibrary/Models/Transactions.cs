using System;
using System.Collections.Generic;

namespace eCommerceClassLibrary.Models
{
    public partial class Transactions
    {
        public int TransactionId { get; set; }
        public string Sku { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public int? TransactionGroup { get; set; }
        public string UserName { get; set; }
        public int? Status { get; set; }
        public string Name { get; set; }
        public string ShippingAddress { get; set; }

        public Products SkuNavigation { get; set; }
        public Users UserNameNavigation { get; set; }
    }
}
