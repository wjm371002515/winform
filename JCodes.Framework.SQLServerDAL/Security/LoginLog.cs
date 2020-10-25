using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.jCodesenum;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace JCodes.Framework.SQLServerDAL
{
	/// <summary>
	/// 对象号: 000204
	/// 用户登录日志信息(LoginLog)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2018-02-08 14:03:32.917
	/// </summary>
	public partial class LoginLog : BaseDALSQLServer<LoginLogInfo>, ILoginLog
	{
		#region 对象实例及构造函数
		public static LoginLog Instance
		{
			get
			{
				return new LoginLog();
			}
		}

		public LoginLog() : base(SQLServerPortal.gc._securityTablePre + "LoginLog", "Id")
		{
			this.sortField = "Id";
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
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.UserId = reader.GetInt32("UserId"); 	 //用户Id
			info.Name = reader.GetString("Name"); 	 //名称
			info.LoginName = reader.GetString("LoginName"); 	 //登录名
			info.CompanyId = reader.GetInt32("CompanyId"); 	 //公司Id
			info.Remark = reader.GetString("Remark"); 	 //备注
			info.IP = reader.GetString("IP"); 	 //IP地址
			info.Mac = reader.GetString("Mac"); 	 //Mac地址
			info.LastUpdateTime = reader.GetDateTime("LastUpdateTime"); 	 //最后更新时间
			info.SystemtypeId = reader.GetString("SystemtypeId"); 	 //系统编号
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(LoginLogInfo obj)
		{
			LoginLogInfo info = obj as LoginLogInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("UserId", info.UserId); 	 //用户Id
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("LoginName", info.LoginName); 	 //登录名
			hash.Add("CompanyId", info.CompanyId); 	 //公司Id
			hash.Add("Remark", info.Remark); 	 //备注
			hash.Add("IP", info.IP); 	 //IP地址
			hash.Add("Mac", info.Mac); 	 //Mac地址
			hash.Add("LastUpdateTime", info.LastUpdateTime); 	 //最后更新时间
			hash.Add("SystemtypeId", info.SystemtypeId); 	 //系统编号
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
			dict.Add("Id", "ID序号");
			dict.Add("UserId", "用户Id");
			dict.Add("Name", "名称");
			dict.Add("LoginName", "登录名");
			dict.Add("CompanyId", "公司Id");
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
        public LoginLogInfo GetLastLoginInfo(Int32 userId)
        {
            string sql = string.Format("Select Top 2 * from {0} where UserId='{1}' order by LastUpdateTime desc", tableName, userId);
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