using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.PlugInInterface;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.AddIn.Other
{
    /// <summary>
    /// 增加一个辅助类，操作和权限系统相关的资源，以便使得权限和工作流相对独立使用。
    /// </summary>
    internal class SecurityHelper
    {
        private static bool InUserList(List<UserInfo> list, UserInfo userInfo)
        {
            bool result = false;
            foreach (UserInfo info in list)
            {
                if (info.ID == userInfo.ID)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 根据用户的登陆名称，获取用户的全名，并放到缓存里面
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetFullNameByName(string name)
        {
            string result = "";
            if (!string.IsNullOrEmpty(name))
            {
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string key = string.Format("{0}-{1}-{2}", method.DeclaringType.FullName, method.Name, name);

                result = MemoryCacheHelper.GetCacheItem<string>(key,
                    delegate() { return BLLFactory<User>.Instance.GetFullNameByName(name); },
                    new TimeSpan(0, 30, 0));//30分钟过期
            }
            return result;
        }
        /// <summary>
        /// 根据用户的ID，获取用户的登陆名称，并放到缓存里面
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetNameByID(string userId)
        {
            string result = "";
            if (!string.IsNullOrEmpty(userId))
            {
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string key = string.Format("{0}-{1}-{2}", method.DeclaringType.FullName, method.Name, userId);

                result = MemoryCacheHelper.GetCacheItem<string>(key,
                    delegate() { return BLLFactory<User>.Instance.GetNameByID(userId.ToInt32()); },
                    new TimeSpan(0, 30, 0));//30分钟过期
            }
            return result;
        }

        /// <summary>
        /// 根据用户的ID，获取用户的全名，并放到缓存里面
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetFullNameByID(string userId)
        {
            string result = "";
            if (!string.IsNullOrEmpty(userId))
            {
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string key = string.Format("{0}-{1}-{2}", method.DeclaringType.FullName, method.Name, userId);

                result = MemoryCacheHelper.GetCacheItem<string>(key,
                    delegate() { return BLLFactory<User>.Instance.GetFullNameByID(userId.ToInt32()); },
                    new TimeSpan(0, 30, 0));//30分钟过期
            }
            return result;
        }

        /// <summary>
        /// 获取用户全部简单对象信息，并放到缓存里面
        /// </summary>
        /// <returns></returns>
        public static List<SimpleUserInfo> GetSimpleUsers()
        {
            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
            string key = string.Format("{0}-{1}", method.DeclaringType.FullName, method.Name);

            return MemoryCacheHelper.GetCacheItem<List<SimpleUserInfo>>(key,
                delegate() { return BLLFactory<User>.Instance.GetSimpleUsers(); },
                new TimeSpan(0, 10, 0));//10分钟过期
        }

        /// <summary>
        /// 获取用户角色集合
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static List<RoleInfo> GetRoleList(int userId)
        {
            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
            string key = string.Format("{0}-{1}-{2}", method.DeclaringType.FullName, method.Name, userId);

            List<RoleInfo> roleList = MemoryCacheHelper.GetCacheItem<List<RoleInfo>>(key,
                delegate() { return BLLFactory<Role>.Instance.GetRolesByUser(userId); },
                new TimeSpan(0, 30, 0));//30分钟过期
            return roleList;
        }

        /// <summary>
        /// 根据当前用户身份，获取对应的顶级机构管理节点。
        /// 如果是超级管理员，返回集团节点；如果是公司管理员，返回其公司节点
        /// </summary>
        /// <returns></returns>
        public static List<OUInfo> GetMyTopGroup(LoginUserInfo userInfo)
        {
            List<OUInfo> list = new List<OUInfo>();
            string key = "Security_MyTopGroup" + userInfo.ID;
            List<OUInfo> returnList = MemoryCacheHelper.GetCacheItem<List<OUInfo>>(key,
                delegate()
                {
                    if (UserInRole(RoleInfo.SuperAdminName, userInfo.ID))
                    {
                        list.AddRange(BLLFactory<OU>.Instance.GetTopGroup());//超级管理员取集团节点
                    }
                    else
                    {
                        OUInfo groupInfo = BLLFactory<OU>.Instance.FindByID(userInfo.CompanyId);//公司管理员取公司节点
                        list.Add(groupInfo);
                    }

                    return list;
                },
                new TimeSpan(0, 30, 0));//30分钟过期
            return returnList;
        }

        /// <summary>
        /// 判断当前用户具有某个角色
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public static bool UserInRole(string roleName, int userId)
        {
            List<RoleInfo> roleList = GetRoleList(userId);
            bool result = false;
            if (roleList != null)
            {
                foreach (RoleInfo info in roleList)
                {
                    if (info.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
