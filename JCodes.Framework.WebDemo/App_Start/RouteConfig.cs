using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JCodes.Framework.WebDemo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //2.自定义路由一：匹配到action
            routes.MapRoute(
                name: "InterfaceRoute",
                url: "{controller}/{action}/{key}",
                defaults: new { key = UrlParameter.Optional }
            );
        }
    }
}