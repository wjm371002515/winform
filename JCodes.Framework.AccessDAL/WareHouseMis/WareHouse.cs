using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.AccessDAL
{
	/// <summary>
	/// WareHouse 的摘要说明�?
	/// </summary>
	public class WareHouse : BaseDALAccess<WareHouseInfo>, IWareHouse
	{
		#region 对象实例及构造函�?

		public static WareHouse Instance
		{
			get
			{
				return new WareHouse();
			}
		}
		public WareHouse() : base("WM_WareHouse","ID")
        {
            this.sortField = "ID";
            this.isDescending = false;
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体�?
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对�?/returns>
		protected override WareHouseInfo DataReaderToEntity(IDataReader dataReader)
		{
			WareHouseInfo wareHouseInfo = new WareHouseInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			wareHouseInfo.ID = reader.GetInt32("ID");
			wareHouseInfo.Name = reader.GetString("Name");
			wareHouseInfo.Manager = reader.GetString("Manager");
			wareHouseInfo.Phone = reader.GetString("Phone");
			wareHouseInfo.Address = reader.GetString("Address");
			wareHouseInfo.Note = reader.GetString("Note");
			
			return wareHouseInfo;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键�?
		/// </summary>
		/// <param name="obj">有效的实体对�?/param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(WareHouseInfo obj)
		{
		    WareHouseInfo info = obj as WareHouseInfo;
			Hashtable hash = new Hashtable(); 
			
 			hash.Add("Name", info.Name);
 			hash.Add("Manager", info.Manager);
 			hash.Add("Phone", info.Phone);
 			hash.Add("Address", info.Address);
 			hash.Add("Note", info.Note);
 				
			return hash;
		}

    }
}