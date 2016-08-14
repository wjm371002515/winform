using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using JCodes.Framework.Common;
using JCodes.Framework.Data.IDAL;

namespace JCodes.Framework.Data.SQLServerDAL
{
    /// <summary>
    /// 用户操作记录（SQL Server数据库实现）
    /// </summary>
    public class UserOperateLog : IUserOperateLog
    {
        /// <summary>
        /// 写入操作日志
        /// </summary>
        public void InsertOperateLog(Model.UserOperateLog userOperateLog)
        {
            string sql = "insert into tbUserOperateLog(UserName,UserIp,OperateInfo,IfSuccess,Description) values (@UserName,@UserIp,@OperateInfo,@IfSuccess,@Description)";
            SqlParameter[] paras = { 
                                   new SqlParameter("@UserName",userOperateLog.UserName),
                                   new SqlParameter("@UserIp",userOperateLog.UserIp),
                                   new SqlParameter("@OperateInfo",userOperateLog.OperateInfo),
                                   new SqlParameter("@IfSuccess",userOperateLog.IfSuccess),
                                   new SqlParameter("@Description",userOperateLog.Description)
                                   };
            SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, sql, paras);
        }

    }
}
