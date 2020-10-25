using JCodes.Framework.Common;
using JCodes.Framework.Common.Encrypt;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.Common.Network;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 用户信息业务管理类
    /// </summary>
    public class User : BaseBLL<UserInfo>
    {
        private IUser dal;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public User() : base()
        {
            if (isMultiDatabase)
            {
                base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, dicmultiDatabase[this.GetType().Name].ToString());
            }
            else
            {
                base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            }

            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//如果需要记录操作日志，则实现这个事件

            dal = baseDal as IUser;
        }

        /// <summary>
        /// 重写删除操作，检查保留管理员用户
        /// </summary>
        /// <param name="key">主键的值</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public override bool DeleteByUser(object key, Int32 userId, DbTransaction trans = null)
        {
            return SetDeletedFlag(key.ToString().ToInt32(), trans);
        }

        /// <summary>
        /// 取消用户的过期设置，变为正常状态
        /// </summary>
        /// <param name="userId"></param>
        internal void CancelExpireByUserId(Int32 userId)
        {
            UserInfo info = this.FindById(userId);
            if (info.IsExpire == (Int16)IsExpire.是)
            {
                info.IsExpire = (Int16)IsExpire.否;
                this.Update(info, info.Id);
            }
        }

        /// <summary>
        /// 获取所有用户的基本信息
        /// </summary>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUsers()
        {
            return dal.GetSimpleUsers();
        }

        /// <summary>
        /// 获取指定ID字符串的用户基本信息
        /// </summary>
        /// <param name="userIds">ID字符串,逗号分开</param>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUsers(string userIds)
        {
            return dal.GetSimpleUsers(userIds);
        }

        /// <summary>
        /// 通过用户机构ID方式获取对应的用户基本信息列表
        /// </summary>
        /// <param name="ouId">用户机构ID方式</param>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUsersByOUId(Int32 ouId)
        {
            return dal.GetSimpleUsersByOUId(ouId);
        }

        /// <summary>
        /// 通过用户角色ID方式获取对应的用户基本信息列表
        /// </summary>
        /// <param name="roleId">用户角色ID</param>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUsersByRoleId(Int32 roleId)
        {
            return dal.GetSimpleUsersByRoleId(roleId);
        }

        /// <summary>
        /// 通过机构ID获取对应的用户列表
        /// </summary>
        /// <param name="ouId">机构ID</param>
        /// <returns></returns>
        public List<UserInfo> GetUsersByOUId(Int32 ouId)
        {
            return dal.GetUsersByOUId(ouId);
        }

        /// <summary>
        /// 通过角色ID获取对应的用户列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        public List<UserInfo> GetUsersByRoleId(Int32 roleId)
        {
            return dal.GetUsersByRoleId(roleId);
        }
               
        /// <summary>
        /// 根据部门ID获取默认机构为该部门的相关人员
        /// </summary>
        /// <param name="ouID">部门ID</param>
        /// <returns></returns>
        public List<UserInfo> FindByDeptId(Int32 deptId)
        {
            string condition = string.Format("DeptId='{0}' ", deptId);
            return base.Find(condition);
        }

        /// <summary>
        /// 根据公司ID获取公司的相关人员
        /// </summary>
        /// <param name="companyId">公司门ID</param>
        /// <returns></returns>
        public List<UserInfo> FindByCompanyId(Int32 companyId)
        {
            string condition = string.Format("CompanyId='{0}' ", companyId);
            return base.Find(condition);
        }

        /// <summary>
        /// 根据公司ID获取公司的相关人员
        /// </summary>
        /// <param name="companyId">公司门ID</param>
        /// <returns></returns>
        public List<SimpleUserInfo> FindSimpleUsersByCompanyId(Int32 companyId)
        {
            string condition = string.Format("CompanyId='{0}' ", companyId);
            return dal.FindSimpleUsers(condition);
        }

        /// <summary>
        /// 根据部门ID获取默认机构为该部门的相关人员
        /// </summary>
        /// <param name="ouID">部门ID</param>
        /// <returns></returns>
        public List<SimpleUserInfo> FindSimpleUsersByDeptId(Int32 deptId)
        {
            string condition = string.Format("DeptId='{0}' ", deptId);
            return dal.FindSimpleUsers(condition);
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
                info = dal.FindSingle(condition);
            }
            return info;
        }

        /// <summary>
        /// 根据用户ID获取用户全名称
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public string GetNameById(Int32 userId)
        {
            return dal.GetNameById(userId);
        }

        /// <summary>
        /// 根据用户登陆名称，获取用户全名
        /// </summary>
        /// <param name="userName">用户登陆名称</param>
        /// <returns></returns>
        public string GetLoginNameByName(string userName)
        {
            return dal.GetLoginNameByName(userName);
        }

        /// <summary>
        /// 获取用户在指定系统类型下的功能集合
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="sessionID"></param>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public List<FunctionInfo> GetUserFunctions(string identity, string sessionId, string systemtypeId)
        {
            string userName = this.GetUserName(identity, sessionId);
            UserInfo userByName = this.GetUserByName(userName);
            List<FunctionInfo> functionsByUser = null;
            if (userByName != null)
            {
                functionsByUser = BLLFactory<Function>.Instance.GetFunctionsByUser(userByName.Id, systemtypeId);
            }
            return functionsByUser;
        }

        public string GetUserName(string identity, string sessionId)
        {
            if ((sessionId == null) || (sessionId == string.Empty))
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
            info.Password = EncodeHelper.DesEncrypt(Const.defaultPwd);
            return base.Insert(obj, trans);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userName">修改用户名</param>
        /// <param name="userPassword">用户密码（未加密）</param>
        /// <param name="systemtypeId">系统类型</param>
        /// <returns></returns>
        public bool ModifyPassword(string userName, string userPassword, string systemtypeId, string ip, string mac)
        {
            bool result = false;
            UserInfo userByName = this.GetUserByName(userName);
            if (userByName != null)
            {
                userPassword = EncodeHelper.DesEncrypt(userPassword);
                userByName.Password = userPassword;

                result = dal.Update(userByName, userByName.Id);
                if (result)
                {
                    //记录用户修改密码日志
                    BLLFactory<LoginLog>.Instance.AddLoginLog(userByName, systemtypeId, ip, mac, "用户修改密码");
                }
            }
            return result;
        }

        /// <summary>
        /// 管理员重置密码
        /// </summary>
        /// <param name="loginUserId">登陆账号ID</param>
        /// <param name="changeUserId">修改账号ID</param>
        /// <param name="systemtypeId">系统类型</param>
        /// <returns></returns>
        public bool ResetPassword(Int32 loginUserId, Int32 changeUserId, string systemtypeId, string ip, string mac)
        {
            bool result = false;
            UserInfo loginInfo = this.FindById(loginUserId);
            UserInfo changeInfo = this.FindById(changeUserId);
            if (loginInfo != null && changeInfo != null)
            {
                //string initPassword = EncryptHelper.ComputeHash("12345678", changeInfo.Name.ToLower());
                string initPassword = EncodeHelper.DesEncrypt(Const.defaultPwd);
                changeInfo.Password = initPassword;
                result = dal.Update(changeInfo, changeInfo.Id);

                if (result)
                {
                    //记录用户修改密码日志
                    string message = string.Format("{0}重置了用户【{1}】的登陆密码", loginInfo.LoginName, changeInfo.LoginName);
                    BLLFactory<LoginLog>.Instance.AddLoginLog(loginInfo, systemtypeId, ip, mac, message);
                }
            }
            return result;
        }

        public override bool Update(UserInfo obj, object primaryKeyValue, DbTransaction trans = null)
        {
            return dal.Update(obj, primaryKeyValue, trans);
        }

        /// <summary>
        /// 根据用户名、密码验证用户身份有效性
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="userPassword">用户密码</param>
        /// <param name="systemtypeId">系统类型ID</param>
        /// <returns></returns>
        public string VerifyUser(string userName, string userPassword, string systemtypeId, string ip, string mac)
        {
            if (string.IsNullOrEmpty(systemtypeId))
            {
                return "";
            }

            string identity = "";
            UserInfo userInfo = this.GetUserByName(userName);
            if (userInfo != null && userInfo.IsExpire == (Int16)IsExpire.否 && userInfo.IsDelete == (Int16)IsDelete.否)
            {
                bool ipAccess = BLLFactory<BlackIP>.Instance.ValidateIPAccess(ip, userInfo.Id);
                if (ipAccess)
                {
                    //userPassword = EncryptHelper.ComputeHash(userPassword, userName.ToLower());
                    Console.WriteLine(string.Format("DEBUG: 用户名:{0}, 明文密码:{1}, 密文密码:{2}, 数据库密码:{3}", userInfo.Name, userPassword, EncodeHelper.DesEncrypt(userPassword), userInfo.Password));
                    userPassword = EncodeHelper.DesEncrypt(userPassword);
                    
                    if (userPassword == userInfo.Password)
                    {
                        //更新用户的登录时间和IP地址
                        dal.UpdateUserLoginData(userInfo.Id, ip, mac);

                        //identity = EncryptHelper.EncryptStr(userName + Convert.ToString(Convert.ToChar(1)) + userPassword, systemType);
                        identity = EncodeHelper.DesEncrypt(userName + Convert.ToString(Convert.ToChar(1)) + userPassword);

                        //记录用户登录日志
                        BLLFactory<LoginLog>.Instance.AddLoginLog(userInfo, systemtypeId, ip, mac, "用户登录");
                    }
                }
                else
                {
                    BLLFactory<LoginLog>.Instance.AddLoginLog(userInfo, systemtypeId, ip, mac, "用户登录操作被黑白名单禁止登陆！");
                }
            }
            return identity;
        }

        /// <summary>
        /// 根据个人图片枚举类型获取图片数据
        /// </summary>
        /// <param name="imagetype">图片枚举类型</param>
        /// <returns></returns>
        public byte[] GetPersonImageBytes(UserImageType imagetype, Int32 userId)
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
        public bool UpdatePersonImageBytes(UserImageType imagetype, Int32 userId, byte[] imageBytes)
        {
           
            return dal.UpdatePersonImageBytes(imagetype, userId, imageBytes);
        }

        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public bool SetDeletedFlag(Int32 Id, DbTransaction trans = null)
        {
            return dal.SetDeletedFlag(Id, trans);
        }
    }
}