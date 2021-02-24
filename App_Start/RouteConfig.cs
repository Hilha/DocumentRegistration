using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DocumentRegistration
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Document",
                url: "document/delete/{id}",
                defaults: new { controller = "Document", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Document",
                url: "document/details/{id}",
                defaults: new { controller = "Document", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Document",
                url: "document/edit/{id}",
                defaults: new { controller = "Document", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Document",
                url: "document/create",
                defaults: new { controller = "Document", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Document",
                url: "documents",
                defaults: new { controller = "Document", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
