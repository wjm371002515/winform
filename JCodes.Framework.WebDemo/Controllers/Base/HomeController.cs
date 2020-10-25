using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using Microsoft.ReportingServices.ReportRendering;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
                ViewBag.LoginName = CurrentUser.LoginName;
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

        /*public ActionResult Index_v3()
        {
            return View();
        }*/

        public ActionResult c8903ed406ee4adf9470638f8eaf6fdc()
        {
            return View();
        }

        public ActionResult mediaplay()
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

        public ActionResult Index_info()
        {
            return View();
        }

        public string saveInfo()
        {
            NameValueCollection nameValueCollection = Request.Form;

            string[] keyrequestArr = nameValueCollection.AllKeys;

            // 表头3个数据，内容13个
            double userCount = 1.0 * (keyrequestArr.Length - 3) / 13;
            if (userCount != (Int32)userCount * 1.0) {
                return "1";
            }

            string xiaoqu = nameValueCollection[keyrequestArr[0]];
            string fanghao = nameValueCollection[keyrequestArr[1]];
            string contactphone = nameValueCollection[keyrequestArr[2]];

            Int32 currIndex = 3;
            List<CollectionUserInfo> lstUserInfo = new List<CollectionUserInfo>();
            for (Int32 i = 0; i < (Int32)userCount; i++) {
                CollectionUserInfo userInfo = new CollectionUserInfo();
                userInfo.Name = nameValueCollection[keyrequestArr[currIndex]];
                currIndex++;
                userInfo.RelationShip = nameValueCollection[keyrequestArr[currIndex]];
                currIndex++;
                userInfo.Gender = (short)EnumHelper.GetMemberValue<Gender>(nameValueCollection[keyrequestArr[currIndex]]);
                currIndex++;
                userInfo.IdCard = nameValueCollection[keyrequestArr[currIndex]];
                currIndex++;
                userInfo.MobilePhone = nameValueCollection[keyrequestArr[currIndex]];
                currIndex++;
                userInfo.HouseHold = nameValueCollection[keyrequestArr[currIndex]];
                currIndex++;
                userInfo.HomeAddress = nameValueCollection[keyrequestArr[currIndex]];
                currIndex++;
                userInfo.IsGoHubei = nameValueCollection[keyrequestArr[currIndex]];
                currIndex++;
                userInfo.IsGoWenzhou = nameValueCollection[keyrequestArr[currIndex]];
                currIndex++;
                userInfo.IsContactHubeiWenzhou = nameValueCollection[keyrequestArr[currIndex]];
                currIndex++;
                userInfo.IsLeaseHubeiWenzhou = nameValueCollection[keyrequestArr[currIndex]];
                currIndex++;
                userInfo.IsHubeiWenzhouVisit = nameValueCollection[keyrequestArr[currIndex]];
                currIndex++;
                userInfo.IsHomeObser = nameValueCollection[keyrequestArr[currIndex]];
                currIndex++;

                lstUserInfo.Add(userInfo);
            }

            return "0";
        }

        public ActionResult ZheSheng_TH()
        {
            return View();
        }

        public ActionResult Index_zheshang_v1()
        {
            return View();
        }
    }
}
