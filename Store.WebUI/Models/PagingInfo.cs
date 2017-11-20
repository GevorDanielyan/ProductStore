using System;


namespace Store.WebUI.Models
{
    public class PagingInfo
    {
        // Quantity of goods
        public int TotalItems { get; set; }

        // Quantity of products on one page
        public int ItemsPerPage { get; set; }

        // Current page number
        public int CurrentPage { get; set; }

        //Total number of pages
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}