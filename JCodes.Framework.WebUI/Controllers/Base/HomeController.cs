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
            ViewBag.SubTitle = "测试界面";

            return View("Second");   
        }

        public ActionResult Index()
        {
            if (CurrentUser != null)
            {
                ViewBag.LoginName = CurrentUser.LoginName;
                ViewBag.Name = CurrentUser.Name;

                StringBuilder sb = new StringBuilder();
                List<MenuInfo> menuList = BLLFactory<Menu>.Instance.GetTopMenu(Const.SystemTypeId);
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
            if (HasFunction(info.AuthGid))
            {
                string url = info.Url;

                string menuId = (i == 0) ? "default" : info.Gid;

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
    }
}
