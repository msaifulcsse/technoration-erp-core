using System;
using Entities.BaseModels;
using Entities.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.EntityModels
{
    public class Product : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }

        [StringLength(20)]
        public string ProductCode { get; set; }

        public string Brand { get; set; }
        public string Size { get; set; }
        public string Variant { get; set; }
        public string Unit { get; set; }

        public string BarcodeNumber { get; set; }
        public string BarcodeImage { get; set; }

        public string QrcodeText { get; set; }
        public string QrcodeImage { get; set; }
    }
}
