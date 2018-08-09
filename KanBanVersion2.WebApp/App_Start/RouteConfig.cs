using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KanBanVersion2.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            
                );
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}/{todoid}",
            //    defaults: new { controller = "Home", action = "TodoDurum", id = UrlParameter.Optional }

            //    );
        }
    }
}
