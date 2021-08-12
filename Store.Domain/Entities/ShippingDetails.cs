using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "What is your name?")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "What is your last name?")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="What is your email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name ="Address")]
        public string Address { get; set; }

        [Display(Name ="Country")]
        public string Country { get; set; }

        [Display(Name ="City")]
        public string City { get; set; }

        [Display(Name ="Phone")]
        public int Phone { get; set; }

        [Display(Name ="PostalCode")]
        public int PostalCode { get; set; }

        public bool GiftWrap { get; set; }
        public ShippingDetails()
        {

        }

    }
}
