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
	/// 对象号: 000013
	/// MVC路由配置项(MVCRouteConfig)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2020-11-24 23:22:15.860
	/// </summary>
	public partial class MVCRouteConfig : BaseDALSQLServer<MVCRouteConfigInfo>, IMVCRouteConfig
	{
		#region 对象实例及构造函数
		public static MVCRouteConfig Instance
		{
			get
			{
				return new MVCRouteConfig();
			}
		}

		public MVCRouteConfig() : base(SQLServerPortal.gc._dicTablePre + "MVCRouteConfig", "Id")
		{
			this.sortField = "Id";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override MVCRouteConfigInfo DataReaderToEntity(IDataReader dataReader)
		{
			MVCRouteConfigInfo info = new MVCRouteConfigInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.Name = reader.GetString("Name"); 	 //名称
			info.Url = reader.GetString("Url"); 	 //URL地址
			info.ModuleInfo = reader.GetString("ModuleInfo"); 	 //模块
			info.OperationInfo = reader.GetString("OperationInfo"); 	 //操作
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(MVCRouteConfigInfo obj)
		{
			MVCRouteConfigInfo info = obj as MVCRouteConfigInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("Url", info.Url); 	 //URL地址
			hash.Add("ModuleInfo", info.ModuleInfo); 	 //模块
			hash.Add("OperationInfo", info.OperationInfo); 	 //操作
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
			dict.Add("Url", "URL地址");
			dict.Add("ModuleInfo", "模块");
			dict.Add("OperationInfo", "操作");
			#endregion
			return dict;
		}
	}
}