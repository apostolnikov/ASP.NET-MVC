using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Twitter.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "MyHomePage",
               url: "",
               defaults: new { controller = "Users", action = "Index", username = "DefaltUsername" }
            );

            routes.MapRoute(
               name: "UserHomePage",
               url: "{username}",
               defaults: new { controller = "Users", action = "ShowUserHomePage", username = "DefaltUsername" }
            );

            routes.MapRoute(
               name: "ShowPartialViews",
               url: "{userId}",
               defaults: new { controller = "Users", action = "DefaltAction", username = "DefaltUserId" }
            );

            routes.MapRoute(
               name: "NotificationsRoute",
               url: "I/Notifications",
               defaults: new { controller = "Notifications", action = "Index"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
