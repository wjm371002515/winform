using JCodes.Framework.Common.Files;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JCodes.Framework.WebUI.Common
{
    public class Portal
    {
        public static HtmlHelpers hh = new HtmlHelpers();
    }

    public class HtmlHelpers
    {
        public string SYSTEMTYPEID = "071bafed-4634-4083-bb34-86dda58edfc4";    // 系统类型
        public string AppUnit = string.Empty;                                   // 单位名称
        public string AppName = string.Empty;                                   // 程序名称
        public string AppWholeName = string.Empty;                              // 单位名称+程序名称
        public bool Registed { get; set; }                                       // 判断是否注册了   
        public bool EnableRegister = false;                                     // 设置一个开关，确定是否要求注册后才能使用软件
        public AppConfig config = new AppConfig();                              // config 全局变量 只一次读取config文件提高效率
        public Boolean IsSuperAdmin = false;                                    // 配置超级管理员

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// 看用户是否具有某个功能
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public bool HasFunction(Dictionary<string, string> cacheDict, string functionId)
        {
            bool result = false;
            if (string.IsNullOrEmpty(functionId))
            {
                result = true;
            }
            else if (cacheDict != null && cacheDict.ContainsKey(functionId))
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 转换框架通用的用户基础信息，方便框架使用
        /// </summary>
        /// <param name="info">权限系统定义的用户信息</param>
        /// <returns></returns>
        public LoginUserInfo ConvertToLoginUser(UserInfo info)
        {
            LoginUserInfo loginInfo = new LoginUserInfo();
            loginInfo.Id = info.Id;
            loginInfo.Name = info.Name;
            loginInfo.LoginName = info.LoginName;
            loginInfo.IdCard = info.IdCard;
            loginInfo.MobilePhone = info.MobilePhone;
            loginInfo.QQ = info.QQ;
            loginInfo.Email = info.Email;
            loginInfo.Gender = info.Gender;
            loginInfo.DeptId = info.DeptId;
            loginInfo.CompanyId = info.CompanyId;
            return loginInfo;
        }
    }
}