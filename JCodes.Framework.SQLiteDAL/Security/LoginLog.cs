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
            this.sortField = "LastUpdated";
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

            info.ID = reader.GetInt32("ID");
            info.User_ID = reader.GetString("User_ID");
            info.LoginName = reader.GetString("LoginName");
            info.FullName = reader.GetString("FullName");
            info.Company_ID = reader.GetString("Company_ID");
            info.CompanyName = reader.GetString("CompanyName");
            info.Note = reader.GetString("Note");
            info.IPAddress = reader.GetString("IPAddress");
            info.MacAddress = reader.GetString("MacAddress");
            info.LastUpdated = reader.GetDateTime("LastUpdated");
            info.SystemType_ID = reader.GetString("SystemType_ID");

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

            hash.Add("User_ID", info.User_ID);
            hash.Add("LoginName", info.LoginName);
            hash.Add("FullName", info.FullName);
            hash.Add("Company_ID", info.Company_ID);
            hash.Add("CompanyName", info.CompanyName);
            hash.Add("Note", info.Note);
            hash.Add("IPAddress", info.IPAddress);
            hash.Add("MacAddress", info.MacAddress);
            hash.Add("LastUpdated", info.LastUpdated);
            hash.Add("SystemType_ID", info.SystemType_ID);

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
            dict.Add("ID", "编号");
            dict.Add("User_ID", "登录用户ID");
            dict.Add("LoginName", "登录名");
            dict.Add("FullName", "真实名称");
            dict.Add("Company_ID", "所属公司ID");
            dict.Add("CompanyName", "所属公司名称");
            dict.Add("Note", "日志描述");
            dict.Add("IPAddress", "IP地址");
            dict.Add("MacAddress", "Mac地址");
            dict.Add("LastUpdated", "更新时间");
            dict.Add("SystemType_ID", "系统编号");
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
            string sql = string.Format("Select * from {0} where User_ID='{1}' order by LastUpdated desc LIMIT 2", tableName, userId);
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