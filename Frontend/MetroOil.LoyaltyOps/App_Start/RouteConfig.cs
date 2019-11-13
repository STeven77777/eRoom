using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MetroOil.LoyaltyOps
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Customers", // Route name
            //    "Customers/ProductPrice", // URL with parameters*
            //    new { controller = "Customers", action = "Index" }
            //    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Account", // Route name
                "Account/{id}", // URL with parameters*
                new { controller = "Account", action = "CardList", id = UrlParameter.Optional }
                );
        }
    }
}
