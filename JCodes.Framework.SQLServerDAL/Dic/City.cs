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
	/// 对象号: 000003
	/// 全国城市表(City)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2018-02-08 13:31:53.726
	/// </summary>
	public partial class City : BaseDALSQLServer<CityInfo>, ICity
	{
		#region 对象实例及构造函数
		public static City Instance
		{
			get
			{
				return new City();
			}
		}

		public City() : base(SQLServerPortal.gc._dicTablePre + "City", "Id")
		{
			this.sortField = "Id";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override CityInfo DataReaderToEntity(IDataReader dataReader)
		{
			CityInfo info = new CityInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.CityName = reader.GetString("CityName"); 	 //城市名字
			info.ZipCode = reader.GetString("ZipCode"); 	 //邮政编码
			info.ProvinceId = reader.GetInt32("ProvinceId"); 	 //省份Id
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(CityInfo obj)
		{
			CityInfo info = obj as CityInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("CityName", info.CityName); 	 //城市名字
			hash.Add("ZipCode", info.ZipCode); 	 //邮政编码
			hash.Add("ProvinceId", info.ProvinceId); 	 //省份Id
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
			dict.Add("CityName", "城市名字");
			dict.Add("ZipCode", "邮政编码");
			dict.Add("ProvinceId", "省份Id");
			#endregion
			return dict;
		}

        /// <summary>
        /// 根据名字查询城市信息
        /// </summary>
        /// <param name="provinceName">省份名字</param>
        /// <returns></returns>
        public List<CityInfo> GetCitysByProvinceName(string provinceName)
        {
            string sql = string.Format("Select c.* from {0}City as c inner join {0}Province as p on c.ProvinceId=p.Id where ProvinceName='{1}' ", SQLServerPortal.gc._dicTablePre, provinceName);
            return base.GetList(sql, null);
        }
	}
}