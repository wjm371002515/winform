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
    public class TimeLineController : RegisterControllers
    {

        /// <summary>
        /// 时间轴
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<SystemTimeLineInfo> lst = BLLFactory<SystemTimeLine>.Instance.GetAll();
            DateTime now = DateTime.Now;
            for (Int32 i = 0; i < lst.Count; i++) {
                Int32 diffdays = DateTimeHelper.GetDiffTime2(lst[i].CreatorTime, now).Days;

                if (diffdays == 0) {
                    lst[i].Data1 = "今天";
                }
                else if (diffdays == 1) {
                    lst[i].Data1 = "昨天";
                }
                else if (diffdays == 2)
                {
                    lst[i].Data1 = "前天";
                }
                else if (diffdays == 7) {
                    lst[i].Data1 = "一周前";
                }
                else if (diffdays == 14)
                {
                    lst[i].Data1 = "两周前";
                }
                else if (diffdays == 21) {
                    lst[i].Data1 = "三周前";
                }
                else if (lst[i].CreatorTime.AddMonths(1).Date == DateTime.Now) {
                    lst[i].Data1 = "一个月前";
                }
                else if (lst[i].CreatorTime.AddMonths(2).Date == DateTime.Now)
                {
                    lst[i].Data1 = "两个月前";
                }
                else if (lst[i].CreatorTime.AddMonths(3).Date == DateTime.Now)
                {
                    lst[i].Data1 = "三个月前";
                }
                else if (lst[i].CreatorTime.AddMonths(4).Date == DateTime.Now)
                {
                    lst[i].Data1 = "四个月前";
                }
                else if (lst[i].CreatorTime.AddMonths(5).Date == DateTime.Now)
                {
                    lst[i].Data1 = "五个月前";
                }
                else if (lst[i].CreatorTime.AddMonths(6).Date == DateTime.Now)
                {
                    lst[i].Data1 = "六个月前";
                }
                else if (lst[i].CreatorTime.AddMonths(7).Date == DateTime.Now)
                {
                    lst[i].Data1 = "七个月前";
                }
                else if (lst[i].CreatorTime.AddMonths(8).Date == DateTime.Now)
                {
                    lst[i].Data1 = "八个月前";
                }
                else if (lst[i].CreatorTime.AddMonths(9).Date == DateTime.Now)
                {
                    lst[i].Data1 = "九个月前";
                }
                else if (lst[i].CreatorTime.AddMonths(10).Date == DateTime.Now)
                {
                    lst[i].Data1 = "十个月前";
                }
                else if (lst[i].CreatorTime.AddMonths(11).Date == DateTime.Now)
                {
                    lst[i].Data1 = "十一个月前";
                }
                else if (lst[i].CreatorTime.AddMonths(12).Date == DateTime.Now)
                {
                    lst[i].Data1 = "十二个月前";
                }
                else if (lst[i].CreatorTime.AddYears(1).Date == DateTime.Now) {
                    lst[i].Data1 = "1年前";
                }
                else if (lst[i].CreatorTime.AddYears(2).Date == DateTime.Now)
                {
                    lst[i].Data1 = "1年前";
                }
                else {
                    lst[i].Data1 = diffdays + "天前";
                }

            }
            return View(lst);
        }
    }
}
