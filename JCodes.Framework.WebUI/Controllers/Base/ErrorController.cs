using JCodes.Framework.BLL;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.WebUI.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCodes.Framework.WebUI.Controllers
{
    /// <summary>
    /// 错误处理的控制器
    /// </summary>
    public class ErrorController : RegisterControllers
    {
        /// <summary>
        /// 报错了默认为没权限
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            SystemLogInfo systemLogInfo = GetUserSystemInfo();
            systemLogInfo.LogLevel = (Int32)LogLevel.LOG_LEVEL_ERR;
            systemLogInfo.ModuleInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            systemLogInfo.OperationInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
            systemLogInfo.Remark = string.Format("403错误 出错的URL地址为: {0}", Request.Url);
            BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo);

            return View("NotAuthor");
        }

        /// <summary>
        /// 未授权页面
        /// </summary>
        /// <returns></returns>
        public ViewResult NotAuthor()
        {
            SystemLogInfo systemLogInfo = GetUserSystemInfo();
            systemLogInfo.LogLevel = (Int32)LogLevel.LOG_LEVEL_ERR;
            systemLogInfo.ModuleInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            systemLogInfo.OperationInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
            systemLogInfo.Remark = string.Format("403错误 出错的URL地址为: {0}", Request.Url);
            BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo);

            return View();
        }

        /// <summary>
        /// 页面没找到错误 404错误
        /// </summary>
        /// <returns></returns>
        public ViewResult NotFound()
        {
            SystemLogInfo systemLogInfo = GetUserSystemInfo();
            systemLogInfo.LogLevel = (Int32)LogLevel.LOG_LEVEL_ERR;
            systemLogInfo.ModuleInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            systemLogInfo.OperationInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
            systemLogInfo.Remark = string.Format("404错误 出错的URL地址为: {0}", Request.Url);
            BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo);

            return View();
        }

         /// <summary>
        /// 服务器内部错误 500错误
        /// </summary>
        /// <returns></returns>
        public ViewResult ServerError()
        {
            SystemLogInfo systemLogInfo = GetUserSystemInfo();
            systemLogInfo.LogLevel = (Int32)LogLevel.LOG_LEVEL_ERR;
            systemLogInfo.ModuleInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            systemLogInfo.OperationInfo = ControllerContext.Controller.ControllerContext.RouteData.Values["action"].ToString(); 
            systemLogInfo.Remark = string.Format("500错误 出错的URL地址为: {0}", Request.Url);
            BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo);

            return View();
        }

    }
}
