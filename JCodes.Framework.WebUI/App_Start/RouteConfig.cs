using JCodes.Framework.BLL;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JCodes.Framework.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Login/Index");
            routes.IgnoreRoute("{*allaspx}", new { allaspx = @".*\.aspx(/.*)?" });
            routes.IgnoreRoute("{*allasp}", new { allasp = @".*\.asp(/.*)?" });
            routes.IgnoreRoute("{*allphp}", new { allphp = @".*\.php(/.*)?" });

            //Default Mvc ignore.
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            /*// 先删除全部的		Route 类型
            for (Int32 i = routes.Count - 1; i >= 0; i--) {
                if (string.Equals(routes[i].GetType().Name, "Route")) {
                    routes.Remove(routes[i]);
                }
            }
            // 路由改成数据库形式
            List<MVCRouteConfigInfo> lst = BLLFactory<MVCRouteConfig>.Instance.GetAll();
            foreach (var route in lst)
            {
                routes.MapRoute(
                    route.Name,
                    route.Url,
                      new { controller = route.ModuleInfo, action = route.OperationInfo}// 参数默认值  
                );

            }*/
            
            routes.MapRoute(
                "首页", // action伪静态  
                 "",
                  new { controller = "Index", action = "Index" }// 参数默认值  
            );

            routes.MapRoute(
               "首页html", // action伪静态  
               "index.html",
               new { controller = "Index", action = "Index" }// 参数默认值  
            );

            routes.MapRoute(
               "首页htm", // action伪静态  
               "index.htm",
               new { controller = "Index", action = "Index" }// 参数默认值  
            );

            routes.MapRoute(
               "首页shtml", // action伪静态  
               "index.shtml",
               new { controller = "Index", action = "Index" }// 参数默认值  
            );

            routes.MapRoute(
               "首页stm", // action伪静态  
               "index.stm",
               new { controller = "Index", action = "Index" }// 参数默认值  
            );

            routes.MapRoute(
               "首页shtm", // action伪静态  
               "index.shtm",
               new { controller = "Index", action = "Index" }// 参数默认值  
            );

            routes.MapRoute(
               "首页asp", // action伪静态  
               "index.asp",
               new { controller = "Index", action = "Index" }// 参数默认值  
            );

            routes.MapRoute(
               "首页aspx", // action伪静态  
               "index.aspx",
               new { controller = "Index", action = "Index" }// 参数默认值  
            );

            routes.MapRoute(
               "首页do", // action伪静态  
               "index.do",
               new { controller = "Index", action = "Index" }// 参数默认值  
            );

            routes.MapRoute(
              "首页php", // action伪静态  
              "index.php",
              new { controller = "Index", action = "Index" }// 参数默认值  
            );

            routes.MapRoute(
               "首页jsp", // action伪静态  
               "index.jsp",
               new { controller = "Index", action = "Index" }// 参数默认值  
            );

            routes.MapRoute(
               "计算日期", // action伪静态  
               "caldate.html",
               new { controller = "Index", action = "CalDate" }// 参数默认值  
            );

            routes.MapRoute(
               "编码转换工具", // action伪静态  
               "codeconversion.html",
               new { controller = "Codeconversion", action = "Index" }// 参数默认值  
            );

            routes.MapRoute(
             "Login", // action伪静态  
             "jLogin.html",
             new { controller = "Login", action = "Index" }// 参数默认值  
            );

            routes.MapRoute(
             "403", // action伪静态  
             "403.html",
             new { controller = "Error", action = "NotAuthor" }// 参数默认值  
           );

            routes.MapRoute(
              "404", // action伪静态  
              "404.html",
              new { controller = "Error", action = "NotFound" }// 参数默认值  
            );

            routes.MapRoute(
               "500", // action伪静态  
               "500.html",
               new { controller = "Error", action = "ServerError" }// 参数默认值  
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}