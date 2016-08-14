using System.Collections.Generic;
using JCodes.Framework.Data.Model;

namespace JCodes.Framework.Data.IDAL
{
    /// <summary>
    /// 用户操作记录接口（不同的数据库访问类实现接口达到多数据库的支持）
    /// </summary>
    public interface IUserOperateLog
    {
        /// <summary>
        /// 写入操作日志
        /// </summary>
        void InsertOperateLog(UserOperateLog userOperateLog);

    }
}
