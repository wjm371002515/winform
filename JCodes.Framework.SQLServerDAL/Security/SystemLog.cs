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
	/// 对象号: 000218
	/// 系统日志表(SystemLog)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2020-11-11 14:54:25.821
	/// </summary>
	public partial class SystemLog : BaseDALSQLServer<SystemLogInfo>, ISystemLog
	{
		#region 对象实例及构造函数
		public static SystemLog Instance
		{
			get
			{
				return new SystemLog();
			}
		}

		public SystemLog() : base(SQLServerPortal.gc._securityTablePre + "SystemLog", "Id")
		{
			this.sortField = "Id";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override SystemLogInfo DataReaderToEntity(IDataReader dataReader)
		{
			SystemLogInfo info = new SystemLogInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.Name = reader.GetString("Name"); 	 //名称
			info.IP = reader.GetString("IP"); 	 //IP地址
			info.Mac = reader.GetString("Mac"); 	 //Mac地址
			info.LogLevel = reader.GetInt16("LogLevel"); 	 //日志级别
			info.CreatorTime = reader.GetDateTime("CreatorTime"); 	 //创建时间
			info.SystemtypeId = reader.GetString("SystemtypeId"); 	 //系统编号
			info.ModuleInfo = reader.GetString("ModuleInfo"); 	 //模块
			info.OperationInfo = reader.GetString("OperationInfo"); 	 //操作
			info.Remark = reader.GetString("Remark"); 	 //备注
			info.SessionId = reader.GetString("SessionId"); 	 //用户后台Session的值
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(SystemLogInfo obj)
		{
			SystemLogInfo info = obj as SystemLogInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("IP", info.IP); 	 //IP地址
			hash.Add("Mac", info.Mac); 	 //Mac地址
			hash.Add("LogLevel", info.LogLevel); 	 //日志级别
			hash.Add("CreatorTime", info.CreatorTime); 	 //创建时间
			hash.Add("SystemtypeId", info.SystemtypeId); 	 //系统编号
			hash.Add("ModuleInfo", info.ModuleInfo); 	 //模块
			hash.Add("OperationInfo", info.OperationInfo); 	 //操作
			hash.Add("Remark", info.Remark); 	 //备注
			hash.Add("SessionId", info.SessionId); 	 //用户后台Session的值
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
			dict.Add("Name", "名称");
			dict.Add("IP", "IP地址");
			dict.Add("Mac", "Mac地址");
			dict.Add("LogLevel", "日志级别");
			dict.Add("CreatorTime", "创建时间");
			dict.Add("SystemtypeId", "系统编号");
			dict.Add("ModuleInfo", "模块");
			dict.Add("OperationInfo", "操作");
			dict.Add("Remark", "备注");
			dict.Add("SessionId", "用户后台Session的值");
			#endregion
			return dict;
		}

        public bool AddSystemLog(SystemLogInfo systemLog)
        {
            string commandText = string.Format("INSERT INTO {0}SystemLog(Name, IP, Mac, Loglevel, CreatorTime, SystemtypeId, ModuleInfo, OperationInfo, Remark, SessionId) VALUES('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')", SQLServerPortal.gc._securityTablePre, systemLog.Name, systemLog.IP, systemLog.Mac, systemLog.LogLevel, systemLog.CreatorTime, systemLog.SystemtypeId, systemLog.ModuleInfo, systemLog.OperationInfo, systemLog.Remark, systemLog.SessionId);
            Database db = CreateDatabase();

            DbCommand command = db.GetSqlStringCommand(commandText);
            if (db.ExecuteNonQuery(command) > 0)
                return true;
            else
                return false;
        }
	}
}