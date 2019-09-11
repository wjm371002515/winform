using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.SQLiteDAL
{
    /// <summary>
    /// 用户登录日志信息
    /// </summary>
    public class LoginLog : BaseDALSQLite<LoginLogInfo>, ILoginLog
    {
        #region 对象实例及构造函数

        public static LoginLog Instance
        {
            get
            {
                return new LoginLog();
            }
        }
        public LoginLog()
            : base(SQLitePortal.gc._securityTablePre+"LoginLog", "ID")
        {
            this.sortField = "LastUpdateTime";
        }

        #endregion

        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// </summary>
        /// <param name="dr">有效的DataReader对象</param>
        /// <returns>实体类对象</returns>
        protected override LoginLogInfo DataReaderToEntity(IDataReader dataReader)
        {
            LoginLogInfo info = new LoginLogInfo();
            SmartDataReader reader = new SmartDataReader(dataReader);

            info.Id = reader.GetInt32("Id");
            info.UserId = reader.GetInt32("UserId");
            info.LoginName = reader.GetString("LoginName");
            info.FullName = reader.GetString("FullName");
            info.CompanyId = reader.GetInt32("CompanyId");
            info.CompanyName = reader.GetString("CompanyName");
            info.Remark = reader.GetString("Remark");
            info.IP = reader.GetString("IP");
            info.Mac = reader.GetString("Mac");
            info.LastUpdateTime = reader.GetDateTime("LastUpdateTime");
            info.SystemtypeId = reader.GetString("SystemtypeId");

            return info;
        }

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(LoginLogInfo obj)
        {
            LoginLogInfo info = obj as LoginLogInfo;
            Hashtable hash = new Hashtable();

            hash.Add("UserId", info.UserId);
            hash.Add("LoginName", info.LoginName);
            hash.Add("FullName", info.FullName);
            hash.Add("CompanyId", info.CompanyId);
            hash.Add("CompanyName", info.CompanyName);
            hash.Add("Remark", info.Remark);
            hash.Add("IP", info.IP);
            hash.Add("Mac", info.Mac);
            hash.Add("LastUpdateTime", info.LastUpdateTime);
            hash.Add("SystemtypeId", info.SystemtypeId);

            return hash;
        }

        /// <summary>
        /// 获取字段中文别名（用于界面显示）的字典集合
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetColumnNameAlias()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            #region 添加别名解析
            dict.Add("Id", "编号");
            dict.Add("UserId", "用户Id");
            dict.Add("LoginName", "登录名");
            dict.Add("FullName", "真实名");
            dict.Add("CompanyId", "公司Id");
            dict.Add("CompanyName", "公司名字");
            dict.Add("Remark", "备注");
            dict.Add("IP", "IP地址");
            dict.Add("Mac", "Mac地址");
            dict.Add("LastUpdateTime", "最后更新时间");
            dict.Add("SystemtypeId", "系统编号");
            #endregion

            return dict;
        }

        /// <summary>
        /// 获取上一次（非刚刚登录）的登录日志
        /// </summary>
        /// <param name="userId">登录用户ID</param>
        /// <returns></returns>
        public LoginLogInfo GetLastLoginInfo(string userId)
        {
            string sql = string.Format("Select * from {0} where UserId='{1}' order by LastUpdateTime desc LIMIT 2", tableName, userId);
            List<LoginLogInfo> list = GetList(sql, null);
            if (list.Count == 2)
            {
                return list[1];
            }
            else
            {
                return null;
            }
        }
    }
}