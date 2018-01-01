using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.Common;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 角色信息业务管理类
    /// </summary>
    public class Role : BaseBLL<RoleInfo>
	{
        /// <summary>
        /// 该ID实际为一个无效ID，当调用FillAdminID会初始化为真是的管理员ID，以后以该实际ID作为管理员的凭证
        /// </summary>
        private static int m_AdminID = -99;
		private IRole roleDal;

        /// <summary>
        /// 构造函数
        /// </summary>
		public Role() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//如果需要记录操作日志，则实现这个事件

			this.roleDal = baseDal as IRole;
		}

        /// <summary>
        /// 根据公司ID（机构ID）获取对应的角色列表
        /// </summary>
        /// <param name="companyId">公司ID（机构ID）</param>
        /// <returns></returns>
        public List<RoleInfo> GetRolesByCompany(string companyId)
        {
            string condition = string.Format("Company_ID='{0}' and Deleted = 0 ", companyId);
            return Find(condition);
        }

        /// <summary>
        /// 为角色添加操作权限
        /// </summary>
        /// <param name="functionID">功能ID</param>
        /// <param name="roleID">角色ID</param>
        public void AddFunction(string functionID, int roleID)
		{
			this.roleDal.AddFunction(functionID, roleID);
		}

        /// <summary>
        /// 为角色添加机构
        /// </summary>
        /// <param name="ouID">机构ID</param>
        /// <param name="roleID">角色ID</param>
		public void AddOU(int ouID, int roleID)
		{
			roleDal.AddOU(ouID, roleID);
		}

        /// <summary>
        /// 为角色添加用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="roleID">角色ID</param>
		public void AddUser(int userID, int roleID)
		{
			this.FillAdminID();
			if (roleID == m_AdminID)
			{
                BLLFactory<User>.Instance.CancelExpire(userID);
			}

			roleDal.AddUser(userID, roleID);
		}
                      
        /// <summary>
        /// 为角色指定新的人员列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="newUserList">人员列表</param>
        /// <returns></returns>
        public bool EditRoleUsers(int roleID, List<int> newUserList)
        {
            return roleDal.EditRoleUsers(roleID, newUserList);
        }
               
        /// <summary>
        /// 为角色指定新的操作功能列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="newFunctionList">功能列表</param>
        /// <returns></returns>
        public bool EditRoleFunctions(int roleID, List<string> newFunctionList)
        {
            return roleDal.EditRoleFunctions(roleID, newFunctionList);
        }

        /// <summary>
        /// 为角色指定新的机构列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="newOUList">机构列表</param>
        /// <returns></returns>
        public bool EditRoleOUs(int roleID, List<int> newOUList)
        {
            return roleDal.EditRoleOUs(roleID, newOUList);
        }

        /// <summary>
        /// 判断Admin用户是否包含用户
        /// </summary>
        /// <returns></returns>
		internal bool AdminHasUser()
		{
			this.FillAdminID();

            return BLLFactory<User>.Instance.GetSimpleUsersByRole(m_AdminID).Count > 0;
		}

        /// <summary>
        /// 检查管理员角色不被移除
        /// </summary>
        /// <param name="roleID"></param>
		private void CanRemoveFromAdmin(int roleID)
		{
			this.FillAdminID();

			if ((roleID == m_AdminID) && (this.GetAdminSimpleUsers().Count <= 1))
			{
				throw new Exception("管理员角色 至少需要包含一个用户！");
			}
		}

        public override bool DeleteByUser(object key, Int32 userId, DbTransaction trans = null)
        {
            this.FillAdminID(trans);

            if (Convert.ToInt32(key) == m_AdminID)
			{
				throw new Exception("管理员角色 不能被删除！");
			}
            return baseDal.DeleteByUser(key, userId, trans);
		}

        /// <summary>
        /// 找到对应的角色名称（管理员），获取其对应的ID作为今后比较
        /// </summary>
		private void FillAdminID(DbTransaction trans = null)
		{
			if (m_AdminID == -99)
			{
                string condition = string.Format("Name='{0}' ", RoleInfo.SuperAdminName);//超级管理员唯一性，不用公司区分
                RoleInfo roleByName = FindSingle(condition, trans);
				if (roleByName != null)
				{
					m_AdminID = roleByName.ID;//保存ID作为管理员角色参考
				}
			}
		}

        /// <summary>
        /// 获取管理员包含的机构ID列表
        /// </summary>
        /// <returns></returns>
		internal List<int> GetAdminOUIDs()
		{
			this.FillAdminID();

			List<OUInfo> oUsByRole =BLLFactory<OU>.Instance.GetOUsByRole(m_AdminID);
            List<int> list = new List<int>();
			foreach (OUInfo info in oUsByRole)
			{
				list.Add(info.ID);
			}
			return list;
		}

        /// <summary>
        /// 获取管理员包含的用户基础信息列表
        /// </summary>
        /// <returns></returns>
        internal List<SimpleUserInfo> GetAdminSimpleUsers()
		{
			this.FillAdminID();

			List<SimpleUserInfo> simpleUsersByRole = BLLFactory<User>.Instance.GetSimpleUsersByRole(m_AdminID);
			int count = simpleUsersByRole.Count;
			if (count <= 1)
			{
				foreach (OUInfo info in BLLFactory<OU>.Instance.GetOUsByRole(m_AdminID))
				{
                    List<SimpleUserInfo> simpleUsersByOU = BLLFactory<User>.Instance.GetSimpleUsersByOU(info.ID);
					if (simpleUsersByOU.Count > 0)
					{
						simpleUsersByRole.Add(simpleUsersByOU[0]);
						count++;
						if (simpleUsersByOU.Count > 1)
						{
							simpleUsersByRole.Add(simpleUsersByOU[1]);
							count++;
						}
						if (count > 1)
						{
							return simpleUsersByRole;
						}
					}
				}
			}
			return simpleUsersByRole;
		}

        /// <summary>
        /// 根据角色名称查找角色对象
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public RoleInfo GetRoleByName(string roleName, string companyId = null, DbTransaction trans = null)
		{
            string condition = string.Format("Name='{0}' ", roleName);
            if (!string.IsNullOrEmpty(companyId))
            {
                condition += string.Format(" And Company_ID='{0}' ", companyId);
            }
			return this.roleDal.FindSingle(condition, trans);
		}

        /// <summary>
        /// 获取对应功能的相关角色列表
        /// </summary>
        /// <param name="functionID">对应功能ID</param>
        /// <returns></returns>
		public List<RoleInfo> GetRolesByFunction(string functionID)
		{
			return this.roleDal.GetRolesByFunction(functionID);
		}

        /// <summary>
        /// 根据机构的ID获取对应的角色列表
        /// </summary>
        /// <param name="ouID">机构的ID</param>
        /// <returns></returns>
        public List<RoleInfo> GetRolesByOU(int ouID)
		{
			return this.roleDal.GetRolesByOU(ouID);
		}

        /// <summary>
        /// 根据用户的ID获取对应的角色列表
        /// </summary>
        /// <param name="userID">用户的ID</param>
        /// <returns></returns>
        public List<RoleInfo> GetRolesByUser(int userID)
		{
			List<RoleInfo> rolesByUser = this.roleDal.GetRolesByUser(userID);

            List<int> list = new List<int>();
			foreach (RoleInfo info in rolesByUser)
			{
                list.Add(info.ID);
			}

            //包含部门中间表的角色
            foreach (OUInfo ouInfo in BLLFactory<OU>.Instance.GetOUsByUser(userID))
			{
				foreach (RoleInfo roleInfo in this.roleDal.GetRolesByOU(ouInfo.ID))
				{
                    if (!list.Contains(roleInfo.ID))
					{
						rolesByUser.Add(roleInfo);
                        list.Add(roleInfo.ID);
					}
				}
			}

            //包含默认所属部门的角色
            UserInfo userInfo = BLLFactory<User>.Instance.FindByID(userID);
            if (userInfo != null)
            {
                foreach (RoleInfo roleInfo in  roleDal.GetRolesByOU(userInfo.Dept_ID.ToInt32()))
                {
                    if (!list.Contains(roleInfo.ID))
                    {
                        rolesByUser.Add(roleInfo);
                        list.Add(roleInfo.ID);
                    }
                }
            }

			return rolesByUser;
		}

        /// <summary>
        /// 从角色操作功能列表中，移除对应的功能
        /// </summary>
        /// <param name="functionID">功能ID</param>
        /// <param name="roleID">角色ID</param>
        public void RemoveFunction(string functionID, int roleID)
		{
			this.roleDal.RemoveFunction(functionID, roleID);
		}

        /// <summary>
        /// 从角色机构列表中，移除指定的机构
        /// </summary>
        /// <param name="ouID">机构ID</param>
        /// <param name="roleID">角色ID</param>
		public void RemoveOU(int ouID, int roleID)
		{
			this.FillAdminID();
			if (roleID == m_AdminID)
			{
                List<SimpleUserInfo> simpleUsersByRole = BLLFactory<User>.Instance.GetSimpleUsersByRole(m_AdminID);
				if (simpleUsersByRole.Count < 1)
				{
					simpleUsersByRole.Clear();
                    List<UserInfo> usersByOU = BLLFactory<User>.Instance.GetUsersByOU(ouID);
					if (usersByOU.Count > 0)
					{
						usersByOU.Clear();
						bool flag = false;
                        List<OUInfo> oUsByRole = BLLFactory<OU>.Instance.GetOUsByRole(m_AdminID);
						foreach (OUInfo info in oUsByRole)
						{
                            if ((info.ID != ouID) && (BLLFactory<User>.Instance.GetSimpleUsersByOU(info.ID).Count > 0))
							{
								flag = true;
								break;
							}
						}
						oUsByRole.Clear();
						if (!flag)
						{
							throw new Exception("管理员角色至少需要包含一个用户！");
						}
					}
				}
			}
			roleDal.RemoveOU(ouID, roleID);
		}

        /// <summary>
        /// 从角色的用户列表中移除指定的用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="roleID">角色ID</param>
		public void RemoveUser(int userID, int roleID)
		{
			this.CanRemoveFromAdmin(roleID);
			this.roleDal.RemoveUser(userID, roleID);
		}

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="obj">角色对象</param>
        /// <param name="primaryKeyValue">主键</param>
        /// <returns></returns>
        public override bool Update(RoleInfo obj, object primaryKeyValue, DbTransaction trans = null)
		{
			if (obj.ID == m_AdminID)
			{
				obj.Name = RoleInfo.SuperAdminName;
			}
            return base.Update(obj, primaryKeyValue, trans);
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
            return roleDal.SetDeletedFlag(id, deleted, trans);
        }
	}
}