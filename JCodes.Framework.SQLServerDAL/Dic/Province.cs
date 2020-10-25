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
	/// 对象号: 000005
	/// 全国省份表(Province)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2018-02-08 13:35:58.474
	/// </summary>
	public partial class Province : BaseDALSQLServer<ProvinceInfo>, IProvince
	{
		#region 对象实例及构造函数
		public static Province Instance
		{
			get
			{
				return new Province();
			}
		}

		public Province() : base(SQLServerPortal.gc._dicTablePre + "Province", "Id")
		{
			this.sortField = "Id";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override ProvinceInfo DataReaderToEntity(IDataReader dataReader)
		{
			ProvinceInfo info = new ProvinceInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.ProvinceName = reader.GetString("ProvinceName"); 	 //省份名称
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(ProvinceInfo obj)
		{
			ProvinceInfo info = obj as ProvinceInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("ProvinceName", info.ProvinceName); 	 //省份名称
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
			dict.Add("ProvinceName", "省份名称");
			#endregion
			return dict;
		}
	}
}