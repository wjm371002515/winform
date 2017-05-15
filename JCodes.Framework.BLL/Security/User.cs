using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 用户信息业务管理类
    /// </summary>
    public class User : BaseBLL<UserInfo>
    {
        private IUser userDal;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public User() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//如果需要记录操作日志，则实现这个事件

            this.userDal = (IUser)base.baseDal;
        }

        /// <summary>
        /// 重写删除操作，检查保留管理员用户
        /// </summary>
        /// <param name="key">主键的值</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public override bool Delete(object key, DbTransaction trans = null)
        {
            List<SimpleUserInfo> adminSimpleUsers = BLLFactory<Role>.Instance.GetAdminSimpleUsers();
            if (adminSimpleUsers.Count == 1)
            {
                SimpleUserInfo info = adminSimpleUsers[0];
                if (Convert.ToInt32(key) == info.ID)
                {
                    throw new Exception("管理员角色至少需要包含一个用户！");
                }
            }
            
            return SetDeletedFlag(key, true, trans);
        }

        /// <summary>
        /// 取消用户的过期设置，变为正常状态
        /// </summary>
        /// <param name="userID"></param>
        internal void CancelExpire(int userID)
        {
            UserInfo info = this.FindByID(userID.ToString());
            if (info.IsExpire)
            {
                info.IsExpire = false;
                this.Update(info, info.ID.ToString());
            }
        }

        /// <summary>
        /// 获取所有用户的基本信息
        /// </summary>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUsers()
        {
            return this.userDal.GetSimpleUsers();
        }

        /// <summary>
        /// 获取指定ID字符串的用户基本信息
        /// </summary>
        /// <param name="userIds">ID字符串,逗号分开</param>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUsers(string userIds)
        {
            return this.userDal.GetSimpleUsers(userIds);
        }

        /// <summary>
        /// 通过用户机构ID方式获取对应的用户基本信息列表
        /// </summary>
        /// <param name="ouID">用户机构ID方式</param>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUsersByOU(int ouID)
        {
            return this.userDal.GetSimpleUsersByOU(ouID);
        }

        /// <summary>
        /// 通过用户角色ID方式获取对应的用户基本信息列表
        /// </summary>
        /// <param name="roleID">用户角色ID</param>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUsersByRole(int roleID)
        {
            return this.userDal.GetSimpleUsersByRole(roleID);
        }

        /// <summary>
        /// 通过机构ID获取对应的用户列表
        /// </summary>
        /// <param name="ouID">机构ID</param>
        /// <returns></returns>
        public List<UserInfo> GetUsersByOU(int ouID)
        {
            return this.userDal.GetUsersByOU(ouID);
        }

        /// <summary>
        /// 通过角色ID获取对应的用户列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        public List<UserInfo> GetUsersByRole(int roleID)
        {
            return this.userDal.GetUsersByRole(roleID);
        }
               
        /// <summary>
        /// 根据部门ID获取默认机构为该部门的相关人员
        /// </summary>
        /// <param name="ouID">部门ID</param>
        /// <returns></returns>
        public List<UserInfo> FindByDept(int ouID)
        {
            string condition = string.Format("Dept_ID='{0}' ", ouID);
            return base.Find(condition);
        }

        /// <summary>
        /// 根据公司ID获取公司的相关人员
        /// </summary>
        /// <param name="companyId">公司门ID</param>
        /// <returns></returns>
        public List<UserInfo> FindByCompany(string companyId)
        {
            string condition = string.Format("Company_ID='{0}' ", companyId);
            return base.Find(condition);
        }

        /// <summary>
        /// 根据公司ID获取公司的相关人员
        /// </summary>
        /// <param name="companyId">公司门ID</param>
        /// <returns></returns>
        public List<SimpleUserInfo> FindSimpleUsersByCompany(string companyId)
        {
            string condition = string.Format("Company_ID='{0}' ", companyId);
            return userDal.FindSimpleUsers(condition);
        }

        /// <summary>
        /// 根据部门ID获取默认机构为该部门的相关人员
        /// </summary>
        /// <param name="ouID">部门ID</param>
        /// <returns></returns>
        public List<SimpleUserInfo> FindSimpleUsersByDept(int ouID)
        {
            string condition = string.Format("Dept_ID='{0}' ", ouID);
            return userDal.FindSimpleUsers(condition);
        }

        /// <summary>
        /// 通过用户登陆名称获取对应的用户信息
        /// </summary>
        /// <param name="userName">用户登陆名称</param>
        /// <returns></returns>
        public UserInfo GetUserByName(string userName)
        {
            UserInfo info = null;
            if (!string.IsNullOrEmpty(userName))
            {
                string condition = string.Format("Name ='{0}' ", userName);
                info = this.userDal.FindSingle(condition);
            }
            return info;
        }

        /// <summary>
        /// 根据用户ID获取用户登陆名称
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public string GetNameByID(int userID)
        {
            return base.GetFieldValue(userID, "Name");
        }

        /// <summary>
        /// 根据用户ID获取用户全名称
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public string GetFullNameByID(int userID)
        {
            return this.userDal.GetFullNameByID(userID);
        }

        /// <summary>
        /// 根据用户登陆名称，获取用户全名
        /// </summary>
        /// <param name="userName">用户登陆名称</param>
        /// <returns></returns>
        public string GetFullNameByName(string userName)
        {
            return this.userDal.GetFullNameByName(userName);
        }

        /// <summary>
        /// 获取用户在指定系统类型下的功能集合
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="sessionID"></param>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public List<FunctionInfo> GetUserFunctions(string identity, string sessionID, string typeID)
        {
            string userName = this.GetUserName(identity, sessionID);
            UserInfo userByName = this.GetUserByName(userName);
            List<FunctionInfo> functionsByUser = null;
            if (userByName != null)
            {
                functionsByUser = BLLFactory<Function>.Instance.GetFunctionsByUser(userByName.ID, typeID);
            }
            return functionsByUser;
        }

        public string GetUserName(string identity, string sessionID)
        {
            if ((sessionID == null) || (sessionID == string.Empty))
            {
                return "";
            }

            string text = Convert.ToString(Convert.ToChar(1));
            identity = EncodeHelper.DesDecrypt(identity);
            int length = identity.IndexOf(text);
            return identity.Substring(0, length);
        }

        public override bool Insert(UserInfo obj, DbTransaction trans = null)
        {
            UserInfo info = (UserInfo)obj;
            //info.Password = EncryptHelper.ComputeHash(info.Password, info.Name.ToLower());
            info.Password = EncodeHelper.DesEncrypt(info.Password);
            return base.Insert(obj, trans);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userName">用户登陆名称</param>
        /// <param name="userPassword">用户密码</param>
        /// <returns></returns>
        public bool ModifyPassword(string userName, string userPassword)
        {
            return ModifyPassword(userName, userPassword, "", "", "");
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userName">修改用户名</param>
        /// <param name="userPassword">用户密码（未加密）</param>
        /// <param name="systemType">系统类型</param>
        /// <param name="ip">IP地址</param>
        /// <param name="macAddr">Mac地址</param>
        /// <returns></returns>
        public bool ModifyPassword(string userName, string userPassword, string systemType, string ip, string macAddr)
        {
            bool result = false;
            UserInfo userByName = this.GetUserByName(userName);
            if (userByName != null)
            {
                //userPassword = EncryptHelper.ComputeHash(userPassword, userName.ToLower());
                userPassword = EncodeHelper.DesEncrypt(userPassword);
                userByName.Password = userPassword;

                result = userDal.Update(userByName, userByName.ID.ToString());
                if (result)
                {
                    //记录用户修改密码日志
                    BLLFactory<LoginLog>.Instance.AddLoginLog(userByName, systemType, ip, macAddr, "用户修改密码");
                }
            }
            return result;
        }

        /// <summary>
        /// 管理员重置密码
        /// </summary>
        /// <param name="loginUser_ID">登陆账号ID</param>
        /// <param name="changeUser_ID">修改账号ID</param>
        /// <param name="ip">登陆IP</param>
        /// <param name="macAddr">登陆Mac地址</param>
        /// <returns></returns>
        public bool ResetPassword(int loginUser_ID, int changeUser_ID, string ip, string macAddr)
        {
            bool result = false;
            UserInfo loginInfo = this.FindByID(loginUser_ID);
            UserInfo changeInfo = this.FindByID(changeUser_ID);
            if (loginInfo != null && changeInfo != null)
            {
                //string initPassword = EncryptHelper.ComputeHash("12345678", changeInfo.Name.ToLower());
                string initPassword = EncodeHelper.DesEncrypt(Const.defaultPwd);
                changeInfo.Password = initPassword;
                result = userDal.Update(changeInfo, changeInfo.ID);

                if (result)
                {
                    //记录用户修改密码日志
                    string message = string.Format("{0}重置了用户【{1}】的登陆密码", loginInfo.FullName, changeInfo.FullName);
                    BLLFactory<LoginLog>.Instance.AddLoginLog(loginInfo, "Security", ip, macAddr, message);
                }
            }
            return result;
        }

        public override bool Update(UserInfo obj, object primaryKeyValue, DbTransaction trans = null)
        {
            if (obj.Password.Length < 50)
            {
                //obj.Password = EncryptHelper.ComputeHash(obj.Password, obj.Name.ToLower());
                obj.Password = EncodeHelper.DesEncrypt(obj.Password);
                
            }
            return this.userDal.Update(obj, primaryKeyValue, trans);
        }

        /// <summary>
        /// 判断用户是否在指定的角色名称中
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public bool UserInRole(string userName, string roleName)
        {
            UserInfo userInfo = this.GetUserByName(userName);
            foreach (RoleInfo info in BLLFactory<Role>.Instance.GetRolesByUser(userInfo.ID))
            {
                if (info.Name == roleName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断用户是否在指定的角色名称中
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public bool UserInRoleById(int userId, string roleName)
        {
            UserInfo userInfo = this.FindByID(userId);
            foreach (RoleInfo info in BLLFactory<Role>.Instance.GetRolesByUser(userInfo.ID))
            {
                if (info.Name == roleName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断用户是否为超级管理员
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <returns></returns>
        public bool UserIsSuperAdmin(string userName)
        {
            return UserInRole(userName, RoleInfo.SuperAdminName);
        }

        /// <summary>
        /// 判断用户是否为管理员，超级管理员、公司级别的系统管理员均通过。
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <returns></returns>
        public bool UserIsAdmin(string userName)
        {
            bool result = UserInRole(userName, RoleInfo.SuperAdminName);
            if (!result)
            {
                result = UserInRole(userName, RoleInfo.CompanyAdminName);
            }
            return result;
        }

        /// <summary>
        /// 根据用户名、密码验证用户身份有效性
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPassword">用户密码</param>
        /// <param name="systemType">系统类型ID</param>
        /// <returns></returns>
        public string VerifyUser(string userName, string userPassword, string systemType)
        {
            return VerifyUser(userName, userPassword, systemType, "", "");
        }

        /// <summary>
        /// 根据用户名、密码验证用户身份有效性
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="userPassword">用户密码</param>
        /// <param name="systemType">系统类型ID</param>
        /// <param name="ip">IP地址</param>
        /// <param name="macAddr">Mac网卡地址</param>
        /// <returns></returns>
        public string VerifyUser(string userName, string userPassword, string systemType, string ip, string macAddr)
        {
            if (string.IsNullOrEmpty(systemType))
            {
                return "";
            }
            string identity = "";
            UserInfo userInfo = this.GetUserByName(userName);
            if (userInfo != null && !userInfo.IsExpire && !userInfo.Deleted)
            {
                bool ipAccess = BLLFactory<BlackIP>.Instance.ValidateIPAccess(ip, userInfo.ID);
                if (ipAccess)
                {
                    //userPassword = EncryptHelper.ComputeHash(userPassword, userName.ToLower());
                    userPassword = EncodeHelper.DesEncrypt(userPassword);
                    
                    if (userPassword == userInfo.Password)
                    {
                        //更新用户的登录时间和IP地址
                        this.userDal.UpdateUserLoginData(userInfo.ID, ip, macAddr);

                        //identity = EncryptHelper.EncryptStr(userName + Convert.ToString(Convert.ToChar(1)) + userPassword, systemType);
                        identity = EncodeHelper.DesEncrypt(userName + Convert.ToString(Convert.ToChar(1)) + userPassword);

                        //记录用户登录日志
                        BLLFactory<LoginLog>.Instance.AddLoginLog(userInfo, systemType, ip, macAddr, "用户登录");
                    }
                }
                else
                {
                    BLLFactory<LoginLog>.Instance.AddLoginLog(userInfo, systemType, ip, macAddr, "用户登录操作被黑白名单禁止登陆！");
                }
            }
            return identity;
        }

        /// <summary>
        /// 根据个人图片枚举类型获取图片数据
        /// </summary>
        /// <param name="imagetype">图片枚举类型</param>
        /// <returns></returns>
        public byte[] GetPersonImageBytes(UserImageType imagetype, int userId)
        {
            IUser dal = baseDal as IUser;
            return dal.GetPersonImageBytes(imagetype, userId);
        }

        /// <summary>
        /// 更新个人相关图片数据
        /// </summary>
        /// <param name="imagetype">图片类型</param>
        /// <param name="userId">用户ID</param>
        /// <param name="imageBytes">图片字节数组</param>
        /// <returns></returns>
        public bool UpdatePersonImageBytes(UserImageType imagetype, int userId, byte[] imageBytes)
        {
            IUser dal = baseDal as IUser;
            return dal.UpdatePersonImageBytes(imagetype, userId, imageBytes);
        }

        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public bool SetDeletedFlag(object id, bool deleted = true, DbTransaction trans = null)
        {
            return userDal.SetDeletedFlag(id, deleted, trans);
        }
    }
}