using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.Common;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 角色信息业务管理类
    /// </summary>
    public class Role : BaseBLL<RoleInfo>
	{
		private IRole dal;

        /// <summary>
        /// 构造函数
        /// </summary>
		public Role() : base()
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

            dal = baseDal as IRole;
		}

        /// <summary>
        /// 根据公司ID（机构ID）获取对应的角色列表
        /// </summary>
        /// <param name="companyId">公司ID（机构ID）</param>
        /// <returns></returns>
        public List<RoleInfo> GetRolesByCompanyId(Int32 companyId)
        {
            string condition = string.Format("CompanyId='{0}' and IsDelete = {1} ", companyId, (short)IsDelete.否);
            return Find(condition);
        }

        /// <summary>
        /// 为角色添加操作权限
        /// </summary>
        /// <param name="functionID">功能ID</param>
        /// <param name="roleID">角色ID</param>
        public void AddFunction(string functionId, Int32 roleId)
		{
            dal.AddFunction(functionId, roleId);
		}

        /// <summary>
        /// 为角色添加机构
        /// </summary>
        /// <param name="ouId">机构ID</param>
        /// <param name="roleId">角色ID</param>
		public void AddOU(Int32 ouId, Int32 roleId)
		{
            dal.AddOU(ouId, roleId);
		}

        /// <summary>
        /// 为角色添加用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID</param>
		public void AddUser(Int32 userId, Int32 roleId)
		{
            if (IsSuperAdmin(userId))
			{
                BLLFactory<User>.Instance.CancelExpireByUserId(userId);
			}

            dal.AddUser(userId, roleId);
		}
                      
        /// <summary>
        /// 为角色指定新的人员列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="newUserList">人员列表</param>
        /// <returns></returns>
        public bool EditRoleUsers(Int32 roleId, List<Int32> newUserList)
        {
            return dal.EditRoleUsers(roleId, newUserList);
        }
               
        /// <summary>
        /// 为角色指定新的操作功能列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="newFunctionList">功能列表</param>
        /// <returns></returns>
        public bool EditRoleFunctions(Int32 roleId, List<string> newFunctionList)
        {
            return dal.EditRoleFunctions(roleId, newFunctionList);
        }

        /// <summary>
        /// 为角色指定新的机构列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="newOUList">机构列表</param>
        /// <returns></returns>
        public bool EditRoleOUs(Int32 roleId, List<Int32> newOUList)
        {
            return dal.EditRoleOUs(roleId, newOUList);
        }

        public override bool DeleteByUser(object key, Int32 userId, DbTransaction trans = null)
        {
            return baseDal.DeleteByUser(key, userId, trans);
		}

        /// <summary>
        /// 找到对应的角色名称（管理员），获取其对应的ID作为今后比较
        /// </summary>
        private bool IsSuperAdmin(Int32 userId, DbTransaction trans = null)
		{
            string userName = BLLFactory<User>.Instance.GetNameById(userId);

            if (BLLFactory<Sysparameter>.Instance.UserIsSuperAdmin(userName))
            {
                return true;
            }
            else if (BLLFactory<Role>.Instance.UserHasRole(userId))
            {
                return false;
            }

            return false;
		}

        /// <summary>
        /// 获取对应功能的相关角色列表
        /// </summary>
        /// <param name="functionGid">对应功能ID</param>
        /// <returns></returns>
		public List<RoleInfo> GetRolesByFunction(string functionGid)
		{
            return dal.GetRolesByFunction(functionGid);
		}

        /// <summary>
        /// 根据机构的ID获取对应的角色列表
        /// </summary>
        /// <param name="ouId">机构的ID</param>
        /// <returns></returns>
        public List<RoleInfo> GetRolesByOU(Int32 ouId)
		{
            return dal.GetRolesByOUId(ouId);
		}

        /// <summary>
        /// 根据用户的ID获取对应的角色列表
        /// </summary>
        /// <param name="userID">用户的ID</param>
        /// <returns></returns>
        public List<RoleInfo> GetRolesByUser(Int32 userId)
		{
            // 1. 用户角色（User_Role）表根据用户Id 查询用户的角色信息
            // 2.1 用户组织（OU_User）表根据用户Id 查询用户所属的部门中间表的组织（工作组）
            // 2.2 查询上一步中的中间件组织（工作组）查询用户的角色信息(OU_Role)
            // 3.1 查询用户对应的部门(DeptId)包含的角色信息OU_Role
            List<RoleInfo> rolesByUser = dal.GetRolesByUserId(userId);

            // 临时变量保存唯一的角色信息
            List<Int32> list = new List<Int32>();
			foreach (RoleInfo info in rolesByUser)
			{
                list.Add(info.Id);
			}

            // 包含部门中间表的角色
            foreach (OUInfo ouInfo in BLLFactory<OU>.Instance.GetOUsByUserId(userId))
            {
                foreach (RoleInfo roleInfo in dal.GetRolesByOUId(ouInfo.Id))
                {
                    if (!list.Contains(roleInfo.Id))
                    {
                        rolesByUser.Add(roleInfo);
                        list.Add(roleInfo.Id);
                    }
                }
            }

            //包含默认所属部门的角色
            UserInfo userInfo = BLLFactory<User>.Instance.FindById(userId);
            if (userInfo != null)
            {
                foreach (RoleInfo roleInfo in dal.GetRolesByOUId(userInfo.DeptId))
                {
                    if (!list.Contains(roleInfo.Id))
                    {
                        rolesByUser.Add(roleInfo);
                        list.Add(roleInfo.Id);
                    }
                }
            }

			return rolesByUser;
		}

        /// <summary>
        /// 从角色操作功能列表中，移除对应的功能
        /// </summary>
        /// <param name="functionGid">功能ID</param>
        /// <param name="roleId">角色ID</param>
        public void RemoveFunction(string functionGid, Int32 roleId)
		{
            dal.RemoveFunction(functionGid, roleId);
		}

        /// <summary>
        /// 从角色机构列表中，移除指定的机构
        /// </summary>
        /// <param name="ouId">机构ID</param>
        /// <param name="roleId">角色ID</param>
		public void RemoveOU(Int32 ouId, Int32 roleId)
		{
            dal.RemoveOU(ouId, roleId);
		}

        /// <summary>
        /// 从角色的用户列表中移除指定的用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID</param>
		public void RemoveUser(Int32 userId, Int32 roleId)
		{
            dal.RemoveUser(userId, roleId);
		}

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="obj">角色对象</param>
        /// <param name="primaryKeyValue">主键</param>
        /// <returns></returns>
        public override bool Update(RoleInfo obj, object primaryKeyValue, DbTransaction trans = null)
		{
            return base.Update(obj, primaryKeyValue, trans);
		}
                       
        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public bool SetDeletedFlag(Int32 id, IsDelete isDelete = IsDelete.是, DbTransaction trans = null)
        {
            return dal.SetDeletedFlag(id, isDelete, trans);
        }

        //<summary>
        //判断用户是否为管理员，超级管理员、公司级别的系统管理员均通过。(作废)
        //</summary>
        //<param name="userName">用户Id</param>
        //<returns></returns>
        public bool UserHasRole(Int32 userId)
        {
            // 1. 用户角色（User_Role）表根据用户Id 查询用户的角色信息
            // 2.1 用户组织（OU_User）表根据用户Id 查询用户所属的部门中间表的组织（工作组）
            // 2.2 查询上一步中的中间件组织（工作组）查询用户的角色信息(OU_Role)
            // 3.1 查询用户对应的部门(DeptId)包含的角色信息OU_Role
            List<RoleInfo> rolesByUser = dal.GetRolesByUserId(userId);

            // 临时变量保存唯一的角色信息
            List<Int32> list = new List<Int32>();
            foreach (RoleInfo info in rolesByUser)
            {
                list.Add(info.Id);
            }

            // 包含部门中间表的角色
            foreach (OUInfo ouInfo in BLLFactory<OU>.Instance.GetOUsByUserId(userId))
            {
                foreach (RoleInfo roleInfo in dal.GetRolesByOUId(ouInfo.Id))
                {
                    if (!list.Contains(roleInfo.Id))
                    {
                        rolesByUser.Add(roleInfo);
                        list.Add(roleInfo.Id);
                    }
                }
            }

            //包含默认所属部门的角色
            UserInfo userInfo = BLLFactory<User>.Instance.FindById(userId);
            if (userInfo != null)
            {
                foreach (RoleInfo roleInfo in dal.GetRolesByOUId(userInfo.DeptId))
                {
                    if (!list.Contains(roleInfo.Id))
                    {
                        rolesByUser.Add(roleInfo);
                        list.Add(roleInfo.Id);
                    }
                }
            }
            // 如果数量大于0 则为配置了角色
            return rolesByUser.Count > Const.Num_Zero ? true : false;
        }
	}
}