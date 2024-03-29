﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Store.Domain.Abstract;

namespace Store.WebUI.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav
        //public ActionResult Index()
        //{
        //    return View();
        //}

        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Products.Select(product => product.Category).Distinct().OrderBy(x => x);

            return PartialView("FlexMenu", categories);
        }
    }
}