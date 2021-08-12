using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Store.Domain.Entities;
using Store.WebUI.Infrastructure.Binders;

namespace Store.WebUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(ShoppingCart), new CartModelBinder());
        }
    }
}
