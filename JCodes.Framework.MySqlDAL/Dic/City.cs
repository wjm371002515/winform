using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.MySqlDAL
{
	/// <summary>
	/// City 的摘要说明。
	/// </summary>
    public class City : BaseDALMySql<CityInfo>, ICity
	{
		#region 对象实例及构造函数

		public static City Instance
		{
			get
			{
				return new City();
			}
		}
		public City() : base(MySqlPortal.gc._basicTablePre+"City","Id")
		{
            IsDescending = false;
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override CityInfo DataReaderToEntity(IDataReader dataReader)
		{
			CityInfo cityInfo = new CityInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);

            cityInfo.Id = reader.GetInt32("Id");
			cityInfo.CityName = reader.GetString("CityName");
			cityInfo.ZipCode = reader.GetString("ZipCode");
            cityInfo.ProvinceId = reader.GetInt32("ProvinceId");
			
			return cityInfo;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(CityInfo obj)
		{
		    CityInfo info = obj as CityInfo;
			Hashtable hash = new Hashtable(); 
			
 			hash.Add("CityName", info.CityName);
 			hash.Add("ZipCode", info.ZipCode);
            hash.Add("ProvinceId", info.ProvinceId);
 				
			return hash;
		}

        public List<CityInfo> GetCitysByProvinceName(string provinceName)
        {
            string sql = string.Format("Select c.* from {0}City c inner join {0}Province p on c.ProvinceId=p.ID where ProvinceName='{1}' ", MySqlPortal.gc._basicTablePre, provinceName);
            return base.GetList(sql, null);
        }
    }
}