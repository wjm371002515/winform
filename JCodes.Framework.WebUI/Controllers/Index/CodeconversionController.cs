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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxCalASCIIToUnicode()
        {
            string stream;
            using (var sr = new StreamReader(Request.InputStream))
            {
                stream = sr.ReadToEnd();
            }

            object json = JsonConvert.DeserializeObject(stream);
           

            SystemLogInfo systemLogInfo = GetUserSystemInfo();
            systemLogInfo.LogLevel = (Int32)LogLevel.LOG_LEVEL_INFO;
            systemLogInfo.ModuleInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            systemLogInfo.OperationInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
            //systemLogInfo.Remark = string.Format("计算日期差 开始日期:{0} 结束日期{1} 结果为:{2}", startdate, enddate, diffDateSpan.Days.ToString());
            BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo);

            return ToJsonContent(new ReturnResult() { ErrorCode = 0, ErrorMessage = "0", LogLevel = (short)LogLevel.LOG_LEVEL_INFO });
        }
    }
}
