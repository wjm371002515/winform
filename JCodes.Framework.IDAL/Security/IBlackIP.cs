using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.IDAL
{
    /// <summary>
    /// 登陆系统的黑白名单列表
    /// </summary>
	public interface IBlackIP : IBaseDAL<BlackIPInfo>
	{              
        /// <summary>
        /// 根据黑名单ID获取对应的用户ID列表
        /// </summary>
        /// <param name="id">黑名单ID</param>
        /// <returns></returns>
        string GetUserIdList(Int32 id);

        void AddUser(Int32 userID, Int32 blackID);

        void RemoveUser(Int32 userID, Int32 blackID);

        void RemoveUserByBlackId(Int32 blackID);
                       
        /// <summary>
        /// 根据用户ID和授权类型获取列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="type">授权类型</param>
        /// <returns></returns>
        List<BlackIPInfo> FindByUser(int userId, AuthrizeType type);
    }
}