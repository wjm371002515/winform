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
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text;

namespace JCodes.Framework.WebUI.Controllers.Base
{
    public class DemoController : RegisterControllers
    {
        public ActionResult c8903ed406ee4adf9470638f8eaf6fdc()
        {
            return View();
        }

        public ActionResult mediaplay()
        {
            return View();
        }

        public ActionResult Index_Statistics()
        {
            return View();
        }

        public ActionResult Index_v4()
        {
            return View();
        }

        public ActionResult Index_v3_detail()
        {
            return View();
        }

        public ActionResult Test_Mobile() {
            return View();
        }

        public JsonResult Build_Html()
        {
            string url = "/Index/Index";

            if (!url.Contains(Request.Url.Host)) {
                url = string.Format("{0}://{1}:{2}/{3}", Request.Url.Scheme, Request.Url.Host, Request.Url.Port, url);
            }

            ReturnResult rr = BuildHtmlPage(url, "index.htm");
            return Json(new { datainfo = rr }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJoke(String Id)
        {
            JokeInfo jokeInfo = BLLFactory<Joke>.Instance.FindById(Id);

            return Json(new { datainfo = jokeInfo }, JsonRequestBehavior.AllowGet);
        }
        
    }
}
