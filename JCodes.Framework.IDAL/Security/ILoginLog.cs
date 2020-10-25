using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;

namespace JCodes.Framework.IDAL
{
    /// <summary>
    /// 用户登录日志信息
    /// </summary>
    public interface ILoginLog : IBaseDAL<LoginLogInfo>
    {
        /// <summary>
        /// 获取上一次（非刚刚登录）的登录日志
        /// </summary>
        /// <param name="userId">登录用户ID</param>
        /// <returns></returns>
        LoginLogInfo GetLastLoginInfo(Int32 userId);
    }
}