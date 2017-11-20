using System;
using Store.Domain.Entities;
using System.Web.Mvc;

namespace Store.WebUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            //Get the Cart object from the session
            ShoppingCart shoppingCart = null;
            if (controllerContext.HttpContext.Session != null)
            {
                shoppingCart = (ShoppingCart)controllerContext.HttpContext.Session[sessionKey];
            }

            //Create a ShoppingCart object if it is not found in the session
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = shoppingCart;
                }
            }

            //Return ShoppingCart object
            return shoppingCart;
        }
    }
}