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
	/// 对象号: 000004
	/// 城市行政区划(District)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2018-02-08 13:34:17.447
	/// </summary>
	public partial class District : BaseDALSQLServer<DistrictInfo>, IDistrict
	{
		#region 对象实例及构造函数

		public static District Instance
		{
			get
			{
				return new District();
			}
		}

		public District() : base(SQLServerPortal.gc._dicTablePre + "District", "Id")
		{
			this.sortField = "Id";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override DistrictInfo DataReaderToEntity(IDataReader dataReader)
		{
			DistrictInfo info = new DistrictInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.DistrictName = reader.GetString("DistrictName"); 	 //行政区划
			info.CityId = reader.GetInt32("CityId"); 	 //城市Id
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(DistrictInfo obj)
		{
			DistrictInfo info = obj as DistrictInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("DistrictName", info.DistrictName); 	 //行政区划
			hash.Add("CityId", info.CityId); 	 //城市Id
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
			dict.Add("DistrictName", "行政区划");
			dict.Add("CityId", "城市Id");
			#endregion
			return dict;
		}
		
		public List<DistrictInfo> GetDistrictByCityName(string cityName)
        {
            string sql = string.Format("Select c.* from {0}District as c inner join {0}City as p on c.CityId=p.Id where CityName='{1}' ", SQLServerPortal.gc._dicTablePre, cityName);
            return base.GetList(sql, null);
        }
	}
}