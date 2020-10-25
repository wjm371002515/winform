using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCodes.Framework.WebUI.Controllers.Index
{
    public class ErrPagesController : Controller
    {
        /// <summary>
        /// 通用配置页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 404 页面不存在错误
        /// </summary>
        /// <returns></returns>
        public ActionResult _404()
        {
            return View();
        }

        /// <summary>
        /// 500 页面内部错误
        /// </summary>
        /// <returns></returns>
        public ActionResult _500()
        {
            return View();
        }
    }
}
