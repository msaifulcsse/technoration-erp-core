using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ViewModels
{
    public class VmProductData
    {
        [Required(ErrorMessage = "Product Id is required")]
        public int ProductId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Provide the product name")]
        public string ProductName { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Provide the product code")]
        public string ProductCode { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Provide the product brand")]
        public string Brand { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Provide the product package size")]
        public string Size { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Provide the product variant")]
        public string Variant { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Provide the product unit")]
        public string Unit { get; set; }

        [StringLength(13)]
        [Required(ErrorMessage = "Provide the product Barcode Text")]
        public string BarcodeNumber { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Provide the product Qrcode Text")]
        public string QRCodeText { get; set; }

        [StringLength(50, ErrorMessage = "Maximum model number limit is 50 characters")]
        public string ModelNumber { get; set; }

        public IFormFile ProductImage { get; set; }

        [StringLength(5000, ErrorMessage = "Maximum description limit is 5000 characters")]
        public string Description { get; set; }

        [StringLength(10, ErrorMessage = "Release date string is allowed within 10 characters")]
        public string ReleaseDate { get; set; }

        [Required(ErrorMessage = "Please select a product status")]
        public bool? ProductStatus { get; set; }
    }
}
