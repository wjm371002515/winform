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
	/// 对象号: 000220
	/// 系统开发时间轴(SystemTimeLine)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2021-01-19 11:05:40.250
	/// </summary>
	public partial class SystemTimeLine : BaseDALSQLServer<SystemTimeLineInfo>, ISystemTimeLine
	{
		#region 对象实例及构造函数
		public static SystemTimeLine Instance
		{
			get
			{
				return new SystemTimeLine();
			}
		}

		public SystemTimeLine() : base(SQLServerPortal.gc._securityTablePre + "SystemTimeLine", "Id")
		{
			this.sortField = "Id";
            this.isDescending = true;
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override SystemTimeLineInfo DataReaderToEntity(IDataReader dataReader)
		{
			SystemTimeLineInfo info = new SystemTimeLineInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.Name = reader.GetString("Name"); 	 //名称
			info.CustomContent = reader.GetString("CustomContent"); 	 //自定义文本
			info.IconCls = reader.GetString("IconCls"); 	 //Icon样式名称
			info.WebSiteUrl = reader.GetString("WebSiteUrl"); 	 //单位网站
			info.DisplayName = reader.GetString("DisplayName"); 	 //显示名称
			info.CreatorTime = reader.GetDateTime("CreatorTime"); 	 //创建时间
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(SystemTimeLineInfo obj)
		{
			SystemTimeLineInfo info = obj as SystemTimeLineInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("CustomContent", info.CustomContent); 	 //自定义文本
			hash.Add("IconCls", info.IconCls); 	 //Icon样式名称
			hash.Add("WebSiteUrl", info.WebSiteUrl); 	 //单位网站
			hash.Add("DisplayName", info.DisplayName); 	 //显示名称
			hash.Add("CreatorTime", info.CreatorTime); 	 //创建时间
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
			dict.Add("CustomContent", "自定义文本");
			dict.Add("IconCls", "Icon样式名称");
			dict.Add("WebSiteUrl", "单位网站");
			dict.Add("DisplayName", "显示名称");
			dict.Add("CreatorTime", "创建时间");
			#endregion
			return dict;
		}
	}
}