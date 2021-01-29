using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Device;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Network;
using JCodes.Framework.Entity;
using JCodes.Framework.WebUI.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using JCodes.Framework.jCodesenum;
using System.Text;

namespace JCodes.Framework.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private Dictionary<String, ErrornoInfo> dicErrInfo = (new JCodes.Framework.Common.ErrorInfo()).GetAllErrorInfo(); 

        /// <summary>
        /// 第一种登陆界面
        /// </summary>
        public ActionResult Index()
        {
            Session.Clear();

            // 设置标题
            AppConfig config = new AppConfig();
            if (config != null)
                ViewBag.AppName = config.AppConfigGet("AppName") + " -";
            ViewBag.SubTitle = "登陆界面";

            return View();
        }

        /// <summary>
        /// 锁屏处理
        /// </summary>
        /// <returns></returns>
        public ActionResult Lock()
        {
            // 设置标题
            AppConfig config = new AppConfig();
            if (config != null)
                ViewBag.AppName = config.AppConfigGet("AppName");
            ViewBag.SubTitle = "登陆界面";

            return View("lockpage");
        }

        /// <summary>
        /// 清空当前用户的Session数据
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearSession()
        {
            Session.Clear();
            ReturnResult rr = new ReturnResult();

            // 系统自动生成错误信息
            rr.ErrorCode = 000000;
            rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
            rr.ErrorMessage = "清楚Session成功";

            return ToJsonContent(rr);
        }

        /// <summary>
        /// 对用户登录的操作进行验证
        /// </summary>
        /// <param name="username">用户账号</param>
        /// <param name="password">用户密码</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public ActionResult CheckUser(string username, string password, string code)
        {
            ReturnResult rr = new ReturnResult();

            bool codeValidated = true;
            if (this.TempData["ValidateCode"] != null)
            {
                codeValidated = (this.TempData["ValidateCode"].ToString() == code);
            }

            if (string.IsNullOrEmpty(username))
            {
                rr.ErrorCode = 100006;
                rr.ErrorMessage = dicErrInfo["E100006"].ChineseName;
                rr.ErrorPath = "LoginController->CheckUser(string username, string password, string code)";
                rr.LogLevel = dicErrInfo["E100006"].LogLevel;
            }
            else if (!codeValidated)
            {
                rr.ErrorCode = 100010;
                rr.ErrorMessage = dicErrInfo["E100010"].ChineseName;
                rr.ErrorPath = "LoginController->CheckUser(string username, string password, string code)";
                rr.LogLevel = dicErrInfo["E100010"].LogLevel;
            }
            else
            {
                string ip = GetClientIp();
                // 这里用USER_AGENT 作为mac信息存入数据库
                string macAddr = Request.ServerVariables["HTTP_USER_AGENT"];

                string identity = BLLFactory<User>.Instance.VerifyUser(username, password, Const.SystemTypeId, ip, macAddr);
                if (!string.IsNullOrEmpty(identity))
                {
                    UserInfo info = BLLFactory<User>.Instance.GetUserByName(username);

                    // 20200221 wjm 调整版本模式同winform
                    // 20191207 wjm 新增判断超级管理员 系统配置参数为1
                    // 20171109 wjm 不应该直接去判断这个Name的值，不合理 删除其逻辑判断
                    if (info != null && BLLFactory<Sysparameter>.Instance.UserIsSuperAdmin(username))
                    {
                        Portal.hh.UserInfo = info;
                        Portal.hh.IsSuperAdmin = true;

                        // 系统自动生成错误信息
                        rr.ErrorCode = 000000;
                        rr.ErrorMessage = "超级管理员登录成功";
                        rr.ErrorPath = "LoginController->CheckUser(string username, string password, string code)";
                        rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
                        rr.Data1 = "超级管理员";
                    }
                    else if (info != null && BLLFactory<Role>.Instance.UserHasRole(info.Id))
                    {
                        Portal.hh.UserInfo = info;
                        Portal.hh.IsSuperAdmin = false;

                        // 系统自动生成错误信息
                        rr.ErrorCode = 000000;
                        rr.ErrorMessage = "登录成功";
                        rr.ErrorPath = "LoginController->CheckUser(string username, string password, string code)";
                        rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
                    }
                    else
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("该用户({0})没有管理员权限", username), typeof(LoginController));

                        // 系统自动生成错误信息
                        rr.ErrorCode = 100001;
                        rr.LogLevel = dicErrInfo["E100001"].LogLevel;
                        rr.ErrorMessage = dicErrInfo["E100001"].ChineseName;
                        rr.ErrorPath = "LoginController->CheckUser(string username, string password, string code)";
                    }

                    if (0 == rr.ErrorCode)
                    {
                        // 加载缓存
                        LoadCache(info);
                    }
                }
                else
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("用户名或密码错误或被禁止登陆[{0}]",username), typeof(LoginController));

                    // 系统自动生成错误信息
                    rr.ErrorCode = 100000;
                    rr.LogLevel = dicErrInfo["E100000"].LogLevel;
                    rr.ErrorMessage = dicErrInfo["E100000"].ChineseName;
                    rr.ErrorPath = "LoginController->CheckUser(string username, string password, string code)";
                    rr.Data1 = username;
                }
            }

            return ToJsonContent(rr);
        }

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        private string GetClientIp()
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

        /// <summary>
        /// 加载缓存
        /// </summary>
        /// <returns></returns>
        public void LoadCache(UserInfo info)
        {
            Dictionary<string, string> functionDict = new Dictionary<string, string>();
            List<FunctionInfo> list = BLLFactory<Function>.Instance.GetFunctionsByUser(info.Id, Portal.hh.SYSTEMTYPEID);
            if (list != null && list.Count > 0)
            {
                functionDict.Clear();
                foreach (FunctionInfo functionInfo in list)
                {
                    if (!functionDict.ContainsKey(functionInfo.DllPath))
                    {
                        functionDict.Add(functionInfo.DllPath, functionInfo.Name);
                    }
                }
            }

            #region 获取角色对应的用户操作部门及公司范围
            List<int> companyLst = BLLFactory<RoleData>.Instance.GetBelongCompanysByUser(info.Id);
            List<int> deptLst = BLLFactory<RoleData>.Instance.GetBelongDeptsByUser(info.Id);
            StringBuilder companysb = new StringBuilder();
            StringBuilder deptsb = new StringBuilder();
            companysb.Append(" in (");
            for (int i = 0; i < companyLst.Count; i++)
            {
                companysb.Append(" '" + companyLst[i] + "', ");
            }
            companysb.Append(" '')");

            if (companyLst.Contains(-1))
            {
                companysb.Append(" or (1 = 1)");
            }

            deptsb.Append(" in (");
            for (int i = 0; i < deptLst.Count; i++)
            {
                deptsb.Append(" '" + deptsb[i] + "', ");
            }
            deptsb.Append(" '')");

            if (deptLst.Contains(-11))
            {
                deptsb.Append(" or (1 = 1)");
            }
            #endregion

            // 并保持到缓存中
            Session["LoginUserInfo"] = Portal.hh.ConvertToLoginUser(info);
            Session["FunctionDict"] = functionDict;
            Session["RoleList"] = BLLFactory<Role>.Instance.GetRolesByUser(info.Id);
            Session["canOptCompanyId"] = companysb.ToString();
            Session["canOptDeptId"] = deptsb.ToString();
            Session["DictData"] = BLLFactory<DictData>.Instance.GetAllDict();
            Session["AppConfig"] = Portal.hh.config;
            // 新增WEB特有
            Session["UserId"] = info.Id;
            Session["UserName"] = info.Name;
            Session["LoginName"] = info.LoginName;
            Session["Identity"] = info.Data1;
        }

        /// <summary>
        /// 验证码的实现
        /// </summary>
        /// <returns>返回验证码</returns>
        public ActionResult CheckCode()
        {
            //首先实例化验证码的类
            MyValidateCode validateCode = new MyValidateCode();
            //生成验证码指定的长度
            //string code = validateCode.CreateValidateCode(5);

            string code = JCodes.Framework.Common.Format.RandomChinese.GetRandomNumber(4, true);
            //将验证码赋值给Session变量
            //Session["ValidateCode"] = code;
            this.TempData["ValidateCode"] = code;
            //创建验证码的图片
            byte[] bytes = validateCode.CreateValidateGraphic(code);
            //最后将验证码返回
            return File(bytes, @"image/jpeg");
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

        public ActionResult GetPortrait(int id)
        {
            ActionResult result = Content("");

            var fileData = BLLFactory<User>.Instance.GetPersonImageBytes(UserImageType.个人肖像, id);
            if (fileData != null)
            {
                result = File(fileData, @"image/png");
            }
            else
            {
                var file = Server.MapPath("/Content/Images/user_male.png");
                fileData = FileUtil.FileToBytes(file);
                result = File(fileData, @"image/png");
            }
            return result;
        }
    }
}
