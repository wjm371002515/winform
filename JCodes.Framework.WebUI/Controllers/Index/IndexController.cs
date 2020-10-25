using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JCodes.Framework.Common.Format;
using Newtonsoft.Json;

namespace JCodes.Framework.WebUI.Controllers.Base
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CalDate() {
            ViewBag.SubTitle = "在线日期时间计算器";
            return View();
        }

        public string AjaxCalDateByDiffDay(string date, Int32 days) {
            if (!ValidateUtil.IsDate(date))
                return JsonConvert.SerializeObject("日期格式错误", Formatting.Indented);
            DateTime resultDT = DateTime.Parse(date).AddDays(days);
            //使用Json.NET的序列号类，能够更加高效、完美
            return JsonConvert.SerializeObject(resultDT.ToString("yyyy月MM年dd日 dddd", new System.Globalization.CultureInfo("zh-CN")), Formatting.Indented);
        }

        public string AjaxCalDaysByDiffDate(string startdate, string enddate)
        {
            if (!ValidateUtil.IsDate(startdate))
                return JsonConvert.SerializeObject("开始日期格式错误", Formatting.Indented);
            if (!ValidateUtil.IsDate(enddate))
                return JsonConvert.SerializeObject("结束日期格式错误", Formatting.Indented);

            TimeSpan diffDateSpan = DateTimeHelper.GetDiffTime2(DateTime.Parse(enddate), DateTime.Parse(startdate));
            //使用Json.NET的序列号类，能够更加高效、完美
            return JsonConvert.SerializeObject(diffDateSpan.Days, Formatting.Indented);
        }

        // TODO Del
        //
        // GET: /Index/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Index/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Index/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Index/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Index/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Index/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Index/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
