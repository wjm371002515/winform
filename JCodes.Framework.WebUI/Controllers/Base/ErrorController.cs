using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCodes.Framework.WebUI.Controllers
{
    /// <summary>
    /// 错误处理的控制器
    /// </summary>
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View("Error");
        }

        public ViewResult NotFound()
        {
            Response.StatusCode = 404;  //you may want to set this to 200
            return View("Building");//View("NotFound");
        }
    }
}
