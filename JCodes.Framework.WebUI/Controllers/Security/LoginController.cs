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

namespace JCodes.Framework.WebUI.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// 第一种登陆界面
        /// </summary>
        public ActionResult Index()
        {
            Session.Clear();

            return View();
        }

        /// <summary>
        /// 锁屏处理
        /// </summary>
        /// <returns></returns>
        public ActionResult Lock()
        {
            return View("lockpage");
        }

        /// <summary>
        /// 清空当前用户的Session数据
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearSession()
        {
            Session.Clear();
            ReturnResult result = new ReturnResult();
            result.ErrorCode = 0;

            return ToJsonContent(result);
        }

        /// <summary>
        /// 第二种登陆界面
        /// </summary>
        public ActionResult SecondIndex()
        {
            Session.Clear();

            return View();
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
            ReturnResult result = new ReturnResult();

            bool codeValidated = true;
            if (this.TempData["ValidateCode"] != null)
            {
                codeValidated = (this.TempData["ValidateCode"].ToString() == code);
            }

            if (string.IsNullOrEmpty(username))
            {
                result.ErrorMessage = "用户名不能为空";
            }
            else if (!codeValidated)
            {
                result.ErrorMessage = "验证码输入有误";
            }
            else
            {
                string ip = NetworkUtil.GetLocalIP();   // TODO 这里要改成requestIP
                string macAddr = HardwareInfoHelper.GetMacAddress();
                string identity = BLLFactory<User>.Instance.VerifyUser(username, password, Const.SystemTypeID, ip, macAddr);
                if (!string.IsNullOrEmpty(identity))
                {
                    UserInfo info = BLLFactory<User>.Instance.GetUserByName(username);
                    if (info != null)
                    {
                        result.ErrorCode = 0;
                        
                        //方便方法使用
                        Session["UserInfo"] = info;

                        Session["FullName"] = info.FullName;
                        Session["UserID"] = info.Id;
                        Session["Company_ID"] = info.CompanyId;
                        Session["Dept_ID"] = info.DeptId;
                        /*bool isSuperAdmin = BLLFactory<User>.Instance.UserInRole(info.Name, RoleInfo.SuperAdminName);//判断是否超级管理员
                        Session["IsSuperAdmin"] = isSuperAdmin;*/

                        Session["Identity"] = info.Name.Trim();

                        #region 取得用户的授权信息，并存储在Session中

                        List<FunctionInfo> functionList = BLLFactory<Functions>.Instance.GetFunctionsByUser(info.Id, Const.SystemTypeID);
                        Dictionary<string, string> functionDict = new Dictionary<string, string>();
                        foreach (FunctionInfo functionInfo in functionList)
                        {
                            /*if (!string.IsNullOrEmpty(functionInfo.FunctionId) &&
                                !functionDict.ContainsKey(functionInfo.FunctionId))
                            {
                                functionDict.Add(functionInfo.FunctionId, functionInfo.Name);
                            }*/
                        }
                        Session["Functions"] = functionDict;

                        #endregion
                    }
                }
                else
                {
                    result.ErrorMessage = "用户名输入错误或者您已经被禁用";
                }
            }

            return ToJsonContent(result);
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
