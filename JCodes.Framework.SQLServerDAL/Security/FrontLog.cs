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
	/// 对象号: 000219
	/// 前台日志(FrontLog)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2020-12-26 08:53:21.119
	/// </summary>
	public partial class FrontLog : BaseDALSQLServer<FrontLogInfo>, IFrontLog
	{
		#region 对象实例及构造函数
		public static FrontLog Instance
		{
			get
			{
				return new FrontLog();
			}
		}

		public FrontLog() : base(SQLServerPortal.gc._securityTablePre + "FrontLog", "Id")
		{
			this.sortField = "Id";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override FrontLogInfo DataReaderToEntity(IDataReader dataReader)
		{
			FrontLogInfo info = new FrontLogInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.ErrorMessage = reader.GetString("ErrorMessage"); 	 //错误信息
			info.ErrorLineNo = reader.GetInt32("ErrorLineNo"); 	 //错误行号
			info.ErrorColumNo = reader.GetInt32("ErrorColumNo"); 	 //错误列号
			info.TimeStamp = reader.GetInt32("TimeStamp"); 	 //时间戳
			info.ErrorPath = reader.GetString("ErrorPath"); 	 //错误路由信息
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(FrontLogInfo obj)
		{
			FrontLogInfo info = obj as FrontLogInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("ErrorMessage", info.ErrorMessage); 	 //错误信息
			hash.Add("ErrorLineNo", info.ErrorLineNo); 	 //错误行号
			hash.Add("ErrorColumNo", info.ErrorColumNo); 	 //错误列号
			hash.Add("TimeStamp", info.TimeStamp); 	 //时间戳
			hash.Add("ErrorPath", info.ErrorPath); 	 //错误路由信息
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
			dict.Add("ErrorMessage", "错误信息");
			dict.Add("ErrorLineNo", "错误行号");
			dict.Add("ErrorColumNo", "错误列号");
			dict.Add("TimeStamp", "时间戳");
			dict.Add("ErrorPath", "错误路由信息");
			#endregion
			return dict;
		}
	}
}