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
    public class IndexController : RegisterControllers
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

        public ActionResult AjaxCalDateByDiffDay(string date, Int32 days)
        {
            if (!ValidateUtil.IsDate(date))
                return ToJsonContent(new ReturnResult() { ErrorCode = 10001, ErrorMessage = "日期格式错误", LogLevel = (short)LogLevel.LOG_LEVEL_INFO });
            DateTime resultDT = DateTime.Parse(date).AddDays(days);

            SystemLogInfo systemLogInfo = GetUserSystemInfo();
            systemLogInfo.LogLevel = (Int32)LogLevel.LOG_LEVEL_INFO;
            systemLogInfo.ModuleInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            systemLogInfo.OperationInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["action"].ToString(); 
            systemLogInfo.Remark = string.Format("计算几天后的日期 日期:{0} 相差{1} 结果为:{2}", date, days, resultDT.ToString("yyyy月MM年dd日 dddd", new System.Globalization.CultureInfo("zh-CN")));
            BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo);

            //使用Json.NET的序列号类，能够更加高效、完美
            return ToJsonContent(new ReturnResult() { ErrorCode = 0, ErrorMessage = resultDT.ToString("yyyy月MM年dd日 dddd", new System.Globalization.CultureInfo("zh-CN")), LogLevel = (short)LogLevel.LOG_LEVEL_INFO });
        }

        public ActionResult AjaxCalDaysByDiffDate(string startdate, string enddate)
        {
            if (!ValidateUtil.IsDate(startdate))
                return ToJsonContent(new ReturnResult() { ErrorCode = 10001, ErrorMessage = "开始日期格式错误", LogLevel = (short)LogLevel.LOG_LEVEL_INFO }); 
            if (!ValidateUtil.IsDate(enddate))
                return ToJsonContent(new ReturnResult() { ErrorCode = 10001, ErrorMessage = "结束日期格式错误", LogLevel = (short)LogLevel.LOG_LEVEL_INFO });

            TimeSpan diffDateSpan = DateTimeHelper.GetDiffTime2(DateTime.Parse(enddate), DateTime.Parse(startdate));

            SystemLogInfo systemLogInfo = GetUserSystemInfo();
            systemLogInfo.LogLevel = (Int32)LogLevel.LOG_LEVEL_INFO;
            systemLogInfo.ModuleInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            systemLogInfo.OperationInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
            systemLogInfo.Remark = string.Format("计算日期差 开始日期:{0} 结束日期{1} 结果为:{2}", startdate, enddate, diffDateSpan.Days.ToString());
            BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo);
           
            return ToJsonContent(new ReturnResult(){ ErrorCode = 0, ErrorMessage = diffDateSpan.Days.ToString(), LogLevel = (short)LogLevel.LOG_LEVEL_INFO});
        }

        /// <summary>
        /// 免责声明
        /// </summary>
        /// <returns></returns>
        public ActionResult Disclaimers()
        {
            return View();
        }

        public ActionResult Test() {
            Int32 a = 0;
            Int32 b = 10 / a;

            return View();
        }

        public ActionResult Test2() {

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            return ToJsonContent(new ReturnResult() { ErrorCode = 0, ErrorMessage = "吴建明测试内容", LogLevel = (short)LogLevel.LOG_LEVEL_INFO });
        }
    }
}
