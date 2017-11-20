using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please, enter the name of the product")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name ="Description")]
        [Required(ErrorMessage = "Please, enter the description of the product")]
        public string Description { get; set; }

        [Display(Name ="Category")]
        [Required(ErrorMessage = "Please, indicate the category for the product")]
        public string Category { get; set; }

        [Display(Name ="Price(eur)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please, enter a positive value for the price")]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
