using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.MySqlDAL
{
	/// <summary>
	/// Province 的摘要说明。
	/// </summary>
    public class Province : BaseDALMySql<ProvinceInfo>, IProvince
	{
		#region 对象实例及构造函数

		public static Province Instance
		{
			get
			{
				return new Province();
			}
		}
		public Province() : base(MySqlPortal.gc._basicTablePre+"Province","Id")
		{
            IsDescending = false;
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override ProvinceInfo DataReaderToEntity(IDataReader dataReader)
		{
			ProvinceInfo provinceInfo = new ProvinceInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);

            provinceInfo.Id = reader.GetInt32("Id");
			provinceInfo.ProvinceName = reader.GetString("ProvinceName");
			
			return provinceInfo;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(ProvinceInfo obj)
		{
		    ProvinceInfo info = obj as ProvinceInfo;
			Hashtable hash = new Hashtable(); 
			
 			hash.Add("ProvinceName", info.ProvinceName);
 				
			return hash;
		}

    }
}