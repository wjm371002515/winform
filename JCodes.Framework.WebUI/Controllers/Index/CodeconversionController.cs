using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JCodes.Framework.Common.Format;
using Newtonsoft.Json;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Framework;
using System.Web.Routing;

namespace JCodes.Framework.WebUI.Controllers.Base
{
    public class CodeconversionController : RegisterControllers
    {

        /// <summary>
        /// 编码格式转换
        /// </summary>
        /// Refer: http://tool.chinaz.com/tools/unicode.aspx
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
