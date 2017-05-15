using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;

namespace JCodes.Framework.OracleDAL
{
	/// <summary>
	/// WareHouse 的摘要说明。
	/// </summary>
	public class WareHouse : BaseDALOracle<WareHouseInfo>, IWareHouse
	{
		#region 对象实例及构造函数

		public static WareHouse Instance
		{
			get
			{
				return new WareHouse();
			}
		}
		public WareHouse() : base("WM_WareHouse","ID")
        {
            this.SeqName = string.Format("SEQ_{0}", tableName);//数值型主键，通过序列生成
            this.sortField = "ID";
            this.isDescending = false;
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override WareHouseInfo DataReaderToEntity(IDataReader dataReader)
		{
			WareHouseInfo info = new WareHouseInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetInt32("ID");
			info.Name = reader.GetString("Name");
			info.Manager = reader.GetString("Manager");
			info.Phone = reader.GetString("Phone");
			info.Address = reader.GetString("Address");
			info.Note = reader.GetString("Note");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(WareHouseInfo obj)
		{
		    WareHouseInfo info = obj as WareHouseInfo;
			Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
 			hash.Add("Name", info.Name);
 			hash.Add("Manager", info.Manager);
 			hash.Add("Phone", info.Phone);
 			hash.Add("Address", info.Address);
 			hash.Add("Note", info.Note);
 				
			return hash;
		}

    }
}