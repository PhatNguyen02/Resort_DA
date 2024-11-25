using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ResortManagement
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Đảm bảo rằng route cho controller Invoice được cấu hình đúng
            routes.MapRoute(
     name: "Invoice",
     url: "Invoice/{action}/{id}",
     defaults: new { controller = "Invoice", action = "Payment", id = UrlParameter.Optional },
     namespaces: new[] { "ResortManagement.Controllers" }
 );


            // Route mặc định
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ResortManagement.Controllers" }
            );
        }

    }
}
