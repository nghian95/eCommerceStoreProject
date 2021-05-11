using System;
using System.Collections.Generic;

namespace eCommerceMVC.Models
{
    public partial class Images
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string OriginalFormat { get; set; }
        public byte[] ImageFile { get; set; }
        public string Sku { get; set; }
    }
}
