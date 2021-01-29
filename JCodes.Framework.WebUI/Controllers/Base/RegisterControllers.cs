using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace JCodes.Framework.WebUI.Controllers.Base
{
    public class RegisterControllers : Controller
    {
        protected string systemtypeId = Const.SystemTypeId;

        protected Dictionary<String, ErrornoInfo> dicErrInfo = (new JCodes.Framework.Common.ErrorInfo()).GetAllErrorInfo(); 

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        { 
            //在Action执行之后执行 输出到输出流中文字：After Action Excutexxx
            Int32 StartExecutedTime = 0;
            if (Session[filterContext.ActionDescriptor.UniqueId] != null) { 
                StartExecutedTime = Convert.ToInt32(Session[filterContext.ActionDescriptor.UniqueId]);
                //清除某个Session
                Session[filterContext.ActionDescriptor.UniqueId] = null;
                Session.Remove(filterContext.ActionDescriptor.UniqueId);
            }
            Int32  ExecutedTime = DateTime.Now.Millisecond - StartExecutedTime;

            SystemLogInfo systemLogInfo = GetUserSystemInfo();
            systemLogInfo.LogLevel = (Int32)LogLevel.LOG_LEVEL_DEBUG;
            systemLogInfo.ModuleInfo = filterContext.Controller.ToString();
            systemLogInfo.OperationInfo = filterContext.ActionDescriptor.ActionName;
            systemLogInfo.Remark = string.Format("结束执行 {0}文件{1}控制器{2}函数 执行时间为{3}微秒", filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.Module, filterContext.Controller.ToString(), filterContext.ActionDescriptor.ActionName, ExecutedTime);

            BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo);

            if (filterContext.Result as ContentResult != null) {
                ReturnResult rr = JsonConvert.DeserializeObject<ReturnResult>((filterContext.Result as ContentResult).Content);
                rr.ErrorPath = string.Format("Controller: {0}-> Action: {1}", filterContext.Controller.ToString(), filterContext.ActionDescriptor.ActionName);
                rr.ExecutedTime = ExecutedTime;
                filterContext.Result = ToJsonContent(rr);
            }
            
            base.OnActionExecuted(filterContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Session[filterContext.ActionDescriptor.UniqueId] = DateTime.Now.Millisecond;

            SystemLogInfo systemLogInfo = GetUserSystemInfo();
            systemLogInfo.LogLevel = (Int32)LogLevel.LOG_LEVEL_DEBUG;
            systemLogInfo.ModuleInfo = filterContext.Controller.ToString();
            systemLogInfo.OperationInfo = filterContext.ActionDescriptor.ActionName;

            //在Action执行前执行
            if (filterContext.ActionParameters.Count == 0){
                systemLogInfo.Remark = string.Format("开始执行 {0}文件{1}控制器{2}函数", filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.Module, filterContext.Controller.ToString(), filterContext.ActionDescriptor.ActionName);
            }
            else{
                StringBuilder sb = new StringBuilder();
                foreach (var d in filterContext.ActionParameters)
                {
                    sb.Append(string.Format("{0}={1},", d.Key, d.Value));
                }

                systemLogInfo.Remark = string.Format("开始执行 {0}文件{1}控制器{2}函数 参数内容为[{3}]", filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.Module, filterContext.Controller.ToString(), filterContext.ActionDescriptor.ActionName, sb.ToString().TrimEnd(','));
            }

            BLLFactory<SystemLog>.Instance.AddSystemLog(systemLogInfo);
            base.OnActionExecuting(filterContext);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        { //在Result执行之后 
            //filterContext.HttpContext.Response.Write(@"<br/>After ViewResult Excute" + "\t " + filterContext);
            base.OnResultExecuted(filterContext);
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        { //在Result执行之前
            //filterContext.HttpContext.Response.Write(@"<br/>Before ViewResult Excute" + "\t " + filterContext);
            base.OnResultExecuting(filterContext);
        }

        /// <summary>
        /// 把object对象转换为ContentResult
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected ContentResult ToJsonContent(object obj)
        {
            string result = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return Content(result);
        }

        #region 获取一些功能的函数 封装

        #region 获取客户端IP地址
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public string GetClientIp()
        {
            //可以透过代理服务器
            string userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(userIP))
            {
                //没有代理服务器,如果有代理服务器获取的是代理服务器的IP
                userIP = Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(userIP))
            {
                userIP = Request.UserHostAddress;
            }

            //替换本机默认的::1
            if (userIP == "::1")
            {
                userIP = "127.0.0.1";
            }

            return userIP;
        }
        #endregion

        #region 设置通用的SystemLogInfo信息
        protected SystemLogInfo GetUserSystemInfo() {
            UserInfo currentUser = Session["UserInfo"] as UserInfo;
            SystemLogInfo systemLogInfo = new SystemLogInfo();
            if (currentUser != null)
                systemLogInfo.Name = currentUser.LoginName;
            else
                systemLogInfo.Name = "非登陆客户";
            systemLogInfo.IP = GetClientIp();
            systemLogInfo.Mac = Request.ServerVariables["HTTP_USER_AGENT"];
            systemLogInfo.CreatorTime = DateTimeHelper.GetServerDateTime2();
            systemLogInfo.SystemtypeId = systemtypeId;
            systemLogInfo.SessionId = Session.SessionID;
            return systemLogInfo;
        }
        #endregion

        #region 生成HTML静态页面
        protected ReturnResult BuildHtmlPage(String url, String htmPageName, Boolean isRootDir = true)
        {
            ReturnResult rr = new ReturnResult();

            // 判断页面URL是否有效
            if (!ValidateUtil.IsValidURL(url))
            {
                rr.ErrorCode = 000033;
                rr.ErrorMessage = dicErrInfo["E000033"].ChineseName;
                rr.ErrorPath = "RegisterControllers->BuildHtmlPage(String url, String htmPageName = null)";
                rr.LogLevel = dicErrInfo["E000033"].LogLevel;
                return rr;
            }

            // 如果页面不为空
            Encoding enc;
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            try
            {
                if (((HttpWebResponse)response).CharacterSet != "ISO-8859-1")
                    enc = Encoding.GetEncoding(((HttpWebResponse)response).CharacterSet);
                else
                    enc = Encoding.UTF8;
            }
            catch
            {
                // *** Invalid encoding passed
                enc = Encoding.UTF8;
            }

            if (enc == null)
                enc = Encoding.Default;

            Stream resstream = response.GetResponseStream();

            StreamReader sr = new StreamReader(resstream, enc);
            string contenthtml = sr.ReadToEnd();

            //压缩  
            contenthtml = Regex.Replace(contenthtml, @"(?<=>)\s|\n|\t(?=<)", string.Empty);
            contenthtml = contenthtml.Trim();

            resstream.Close();
            sr.Close(); //写入文件
            System.IO.StreamWriter sw;
            if (isRootDir)
                sw = new System.IO.StreamWriter(Server.MapPath("~/" + htmPageName), false, enc);
            else
                sw = new System.IO.StreamWriter(Server.MapPath(htmPageName), false, enc);

            sw.Write(contenthtml);
            sw.Close();

            rr.ErrorCode = 000000;
            rr.ErrorMessage = "生成页面成功";
            rr.ErrorPath = "RegisterControllers->BuildHtmlPage(String url, String htmPageName = null)";
            rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
            return rr;
        }

        #endregion
        #endregion
    }
}
