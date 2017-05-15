using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace JCodes.Framework.IDAL
{
    public interface IRole : IBaseDAL<RoleInfo>
	{
		void AddFunction(string functionID, int roleID);
		void AddOU(int ouID, int roleID);
		void AddUser(int userID, int roleID);
              
        /// <summary>
        /// 为角色指定新的人员列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="newUserList">人员列表</param>
        /// <returns></returns>
        bool EditRoleUsers(int roleID, List<int> newUserList);
                
        /// <summary>
        /// 为角色指定新的操作功能列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="newFunctionList">功能列表</param>
        /// <returns></returns>
        bool EditRoleFunctions(int roleID, List<string> newFunctionList);

        /// <summary>
        /// 为角色指定新的机构列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="newOUList">机构列表</param>
        /// <returns></returns>
        bool EditRoleOUs(int roleID, List<int> newOUList);

        List<RoleInfo> GetRolesByFunction(string functionID);
        List<RoleInfo> GetRolesByOU(int ouID);
        List<RoleInfo> GetRolesByUser(int userID);
		void RemoveFunction(string functionID, int roleID);
		void RemoveOU(int ouID, int roleID);
		void RemoveUser(int userID, int roleID);

        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        bool SetDeletedFlag(object id, bool deleted = true, DbTransaction trans = null);
	}
}