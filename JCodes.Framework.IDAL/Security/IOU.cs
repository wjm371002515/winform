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
    public interface IOU : IBaseDAL<OUInfo>
	{
        void AddUser(Int32 userId, Int32 ouId);
        List<OUInfo> GetOUsByRoleId(Int32 roleId, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否);
        List<OUInfo> GetOUsByUser(Int32 userId, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否);

        void RemoveUser(Int32 userId, Int32 ouId);
                        
        /// <summary>
        /// 根据指定机构节点ID，获取其下面所有机构列表
        /// </summary>
        /// <param name="parentId">指定机构节点ID</param>
        /// <returns></returns>
        List<OUInfo> GetAllOUsByParent(Int32 parentId, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否);

        /// <summary>
        /// 获取树形结构的机构列表
        /// </summary>
        List<OUNodeInfo> GetTree(IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否);

        /// <summary>
        /// 获取指定机构下面的树形列表
        /// </summary>
        /// <param name="mainOUId">指定机构ID</param>
        List<OUNodeInfo> GetTreeById(int mainOUId, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否);

        /// <summary>
        /// 获取机构的名称
        /// </summary>
        /// <param name="id">机构ID</param>
        /// <returns></returns>
        string GetName(Int32 Id, DbTransaction trans = null);
                      
        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        bool SetDeletedFlag(Int32 Id, DbTransaction trans = null);
                        
        /// <summary>
        /// 为机构制定新的人员列表
        /// </summary>
        /// <param name="ouID">机构ID</param>
        /// <param name="newUserList">人员列表</param>
        /// <returns></returns>
        bool EditOuUsers(Int32 ouId, List<Int32> newUserList);
    }
}