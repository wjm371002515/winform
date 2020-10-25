using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace JCodes.Framework.IDAL
{
    public interface IRole : IBaseDAL<RoleInfo>
	{
        void AddFunction(string functionGid, Int32 roleId);
        void AddOU(Int32 ouId, Int32 roleId);
        void AddUser(Int32 userId, Int32 roleId);
              
        /// <summary>
        /// 为角色指定新的人员列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="newUserList">人员列表</param>
        /// <returns></returns>
        bool EditRoleUsers(Int32 roleId, List<Int32> newUserList);
                
        /// <summary>
        /// 为角色指定新的操作功能列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="newFunctionList">功能列表</param>
        /// <returns></returns>
        bool EditRoleFunctions(Int32 roleId, List<string> newFunctionList);

        /// <summary>
        /// 为角色指定新的机构列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="newOUList">机构列表</param>
        /// <returns></returns>
        bool EditRoleOUs(Int32 roleId, List<Int32> newOUList);

        List<RoleInfo> GetRolesByFunction(string functionGid, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否);
        List<RoleInfo> GetRolesByOUId(Int32 ouId, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否);
        List<RoleInfo> GetRolesByUserId(Int32 userId, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否);
        void RemoveFunction(string functionGid, Int32 roleId);
        void RemoveOU(Int32 ouId, Int32 roleId);
        void RemoveUser(Int32 userId, Int32 roleId);

        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        bool SetDeletedFlag(Int32 id, IsDelete isDelete = IsDelete.是, DbTransaction trans = null);
	}
}