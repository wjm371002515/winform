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
	/// 对象号: 000217
	/// 系统授权认证(SystemAuthorize)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2019-09-27 09:44:19.133
	/// </summary>
	public partial class SystemAuthorize : BaseDALSQLServer<SystemAuthorizeInfo>, ISystemAuthorize
	{
		#region 对象实例及构造函数
		public static SystemAuthorize Instance
		{
			get
			{
				return new SystemAuthorize();
			}
		}

		public SystemAuthorize() : base(SQLServerPortal.gc._securityTablePre + "SystemAuthorize", "Id")
		{
			this.sortField = "Id";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override SystemAuthorizeInfo DataReaderToEntity(IDataReader dataReader)
		{
			SystemAuthorizeInfo info = new SystemAuthorizeInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.SystemtypeId = reader.GetString("SystemtypeId"); 	 //系统编号
			info.Licence = reader.GetString("Licence"); 	 //许可证
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(SystemAuthorizeInfo obj)
		{
			SystemAuthorizeInfo info = obj as SystemAuthorizeInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("SystemtypeId", info.SystemtypeId); 	 //系统编号
			hash.Add("Licence", info.Licence); 	 //许可证
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
			dict.Add("SystemtypeId", "系统编号");
			dict.Add("Licence", "许可证");
			#endregion
			return dict;
		}
	}
}