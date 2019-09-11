using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Entity;
using Microsoft.ReportingServices.ReportRendering;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JCodes.Framework.WebDemo.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            AppConfig config = Cache.Instance["AppConfig"] as AppConfig;
            if (config == null)
            {
                config = new AppConfig();
                Cache.Instance["AppConfig"] = config;
            }

            ViewBag.Title = config.AppConfigGet("AppName");
            //List<MachinesInfo> lstmachines = new JCodes.Framework.BLL.Machines().GetAll();

            /*if (CurrentUser != null)
            {
                ViewBag.FullName = CurrentUser.FullName;
                ViewBag.Name = CurrentUser.Name;

                StringBuilder sb = new StringBuilder();
                List<MenuInfo> menuList = BLLFactory<Menus>.Instance.GetTopMenu(Const.SystemTypeID);
                int i = 0;
                foreach (MenuInfo menuInfo in menuList)
                {
                    sb.Append(GetMenuItemString(menuInfo, i));
                    i++;
                }
                ViewBag.HeaderScript = sb.ToString();//一级菜单代码
            }*/
            return View();            
        }

        public ActionResult Index_v1()
        {
            return View();
        }

        public ActionResult Index_v2()
        {
            return View();
        }

        public ActionResult Index_v3()
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

        public ActionResult Index_Statistics()
        {
            return View();
        }

        public ActionResult Index_add()
        {
            return View();
        }

        public ActionResult Index_calcDemo()
        {
            return View();
        }


        /*
         * 1. Type 默认值
         */
        public string Calc()
        {
            return "abc";
        }
    }
}
