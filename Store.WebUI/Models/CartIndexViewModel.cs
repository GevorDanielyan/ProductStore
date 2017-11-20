using System;
using Store.Domain.Entities;

namespace Store.WebUI.Models
{
    public class CartIndexViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public string ReturnUrl { get; set; }
    }
}