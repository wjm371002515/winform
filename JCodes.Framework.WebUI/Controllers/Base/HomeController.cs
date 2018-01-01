using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JCodes.Framework.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Second()
        {
            return View("Second");   
        }

        public ActionResult Index()
        {
            if (CurrentUser != null)
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
            }
            return View();            
        }

        private string GetMenuItemString(MenuInfo info, int i)
        {
            string result = "";
            if (HasFunction(info.FunctionId))
            {
                string url = info.Url;
                if (url != null)
                {
                    // url = url.Replace("#", "");
                }

                string menuId = (i == 0) ? "default" : info.ID.ToString();

                if (!string.IsNullOrEmpty(url))
                {
                    result = string.Format("<li><a href=\"#\" onclick=\"showSubMenu('{0}', '{1}', '{2}')\">{3}</a></li>", info.Url, info.Name, menuId, info.Name);
                }
                else
                {
                    result = string.Format("<li><a href=\"#\" onclick=\"showSubMenu('{2}', '用户管理', '{0}')\">{1}</a></li>", menuId, info.Name, info.Url);
                }
            }
            return result;
        }

        public ActionResult Another()
        {
            if (CurrentUser != null)
            {
                ViewBag.FullName = CurrentUser.FullName;
                ViewBag.Name = CurrentUser.Name;
            }
            return View();
        }
    }
}
