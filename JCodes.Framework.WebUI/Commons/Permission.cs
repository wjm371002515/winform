using System;
using System.Web;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.BLL;

namespace JCodes.Framework.WebUI.Common
{
	/// <summary>
    /// 权限控制类
	/// </summary>
	public class Permission
	{		
		/// <summary>
		/// 判断当前用户是否拥有某功能点的权限
		/// </summary>
		/// <param name="functionId"></param>
		/// <returns></returns>
		public static bool HasFunction(string functionId)
		{
            bool hasFunction = false;

            UserInfo CurrentUser = HttpContext.Current.Session["UserInfo"] as UserInfo;
            if (CurrentUser != null && CurrentUser.Name == "admin")
            {
                hasFunction = true;
            }
            else
            {
                if (string.IsNullOrEmpty(functionId))
                {
                    hasFunction = true;
                }
                else
                {
                    Dictionary<string, string> functionDict = HttpContext.Current.Session["Functions"] as Dictionary<string, string>;
                    if (functionDict != null && functionDict.ContainsKey(functionId))
                    {
                        hasFunction = true;
                    }
                }
            }
            return hasFunction;
		}

        /// <summary>
        /// 判断是否为系统管理员
        /// </summary>
        /// <returns>true:系统管理员,false:不是系统管理员</returns>
        public static bool IsAdmin()
        {
            bool blnIsAdmin = false;
            UserInfo CurrentUser = HttpContext.Current.Session["UserInfo"] as UserInfo;
            if (CurrentUser != null)
            {
                //int groupID = Permission.CurrentUser.Dept_id;
                //string topGroupName = BLLFactory<Group>.Instance.GetTopGroupName(groupID);
                //if (topGroupName == "停车场管理处")
                //{
                //    blnIsAdmin = true;
                //}
                //else
                {
                    List<RoleInfo> roleList = BLLFactory<Role>.Instance.GetRolesByUser(CurrentUser.ID);
                    if (roleList != null)
                    {
                        foreach (RoleInfo info in roleList)
                        {
                            if (info.Name == "系统管理员")
                            {
                                blnIsAdmin = true;
                                break;
                            }
                        }
                    }
                }
            }
            return blnIsAdmin;
        }
	}

    public class TempPermit
    {
        public string LoginName { get; set; }
        public string CipherText { get; set; }
        public string PlainText { get; set; }
        public DateTime LoginTime { get; set; }
    }
}