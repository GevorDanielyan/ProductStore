using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Models;

namespace Store.WebUI.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        //public ActionResult Index()
        //{
        //    return View();
        //}

        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(ShoppingCart shoppingCart, ShippingDetails shippingDetails)
        {
            if (shoppingCart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your shopping cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(shoppingCart, shippingDetails);
                shoppingCart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public ViewResult Index(ShoppingCart shoppingCart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                ShoppingCart = shoppingCart,
                ReturnUrl = returnUrl
            });
        }

        public PartialViewResult Summary(ShoppingCart shoppingCart)
        {
            return PartialView(shoppingCart);
        }

        public RedirectToRouteResult AddToCart(ShoppingCart shoppingCart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(g => g.ProductId == productId);

            if (product != null)
            {
                shoppingCart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(ShoppingCart shoppingCart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(g => g.ProductId == productId);

            if (product != null)
            {
                shoppingCart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}