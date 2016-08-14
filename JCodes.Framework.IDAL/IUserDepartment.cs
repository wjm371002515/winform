﻿using System.Collections.Generic;
using JCodes.Framework.Data.Model;

namespace JCodes.Framework.Data.IDAL
{
    /// <summary>
    /// 用户部门接口（不同的数据库访问类实现接口达到多数据库的支持）
    /// </summary>
    public interface IUserDepartment
    {
        /// <summary>
        /// 设置用户部门（单个用户）
        /// </summary>
        /// <param name="dep_addList">要增加的</param>
        /// <param name="dep_deleteList">要删除的</param>
        bool SetDepartmentSingle(List<UserDepartment> dep_addList, List<UserDepartment> dep_deleteList);

        /// <summary>
        /// 设置用户部门（批量设置）
        /// </summary>
        /// <param name="dep_addList">要增加的</param>
        /// <param name="dep_deleteList">要删除的</param>
        bool SetDepartmentBatch(List<UserDepartment> dep_addList, List<UserDepartment> dep_deleteList);

    }
}
