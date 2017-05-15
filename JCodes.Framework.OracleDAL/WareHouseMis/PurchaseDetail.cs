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
	/// PurchaseDetail 的摘要说明。
	/// </summary>
	public class PurchaseDetail : BaseDALOracle<PurchaseDetailInfo>, IPurchaseDetail
	{
		#region 对象实例及构造函数

		public static PurchaseDetail Instance
		{
			get
			{
				return new PurchaseDetail();
			}
		}
		public PurchaseDetail() : base("WM_PurchaseDetail","ID")
        {
            this.SeqName = string.Format("SEQ_{0}", tableName);//数值型主键，通过序列生成
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override PurchaseDetailInfo DataReaderToEntity(IDataReader dataReader)
		{
			PurchaseDetailInfo info = new PurchaseDetailInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetInt32("ID");
			info.PurchaseHead_ID = reader.GetInt32("PurchaseHead_ID");
			info.OperationType = reader.GetString("OperationType");
			info.ItemNo = reader.GetString("ItemNo");
			info.ItemName = reader.GetString("ItemName");
			info.MapNo = reader.GetString("MapNo");
			info.Specification = reader.GetString("Specification");
			info.Material = reader.GetString("Material");
			info.ItemBigType = reader.GetString("ItemBigType");
			info.ItemType = reader.GetString("ItemType");
			info.Unit = reader.GetString("Unit");
			info.Price = reader.GetDecimal("Price");
			info.Quantity = reader.GetDouble("Quantity");
			info.Amount = reader.GetDecimal("Amount");
			info.Source = reader.GetString("Source");
			info.StoragePos = reader.GetString("StoragePos");
			info.UsagePos = reader.GetString("UsagePos");
            info.WareHouse = reader.GetString("WareHouse");
            info.Dept = reader.GetString("Dept");

			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(PurchaseDetailInfo obj)
		{
		    PurchaseDetailInfo info = obj as PurchaseDetailInfo;
			Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
 			hash.Add("PurchaseHead_ID", info.PurchaseHead_ID);
 			hash.Add("OperationType", info.OperationType);
 			hash.Add("ItemNo", info.ItemNo);
 			hash.Add("ItemName", info.ItemName);
 			hash.Add("MapNo", info.MapNo);
 			hash.Add("Specification", info.Specification);
 			hash.Add("Material", info.Material);
 			hash.Add("ItemBigType", info.ItemBigType);
 			hash.Add("ItemType", info.ItemType);
 			hash.Add("Unit", info.Unit);
 			hash.Add("Price", info.Price);
 			hash.Add("Quantity", info.Quantity);
 			hash.Add("Amount", info.Amount);
 			hash.Add("Source", info.Source);
 			hash.Add("StoragePos", info.StoragePos);
 			hash.Add("UsagePos", info.UsagePos);
            hash.Add("WareHouse", info.WareHouse);
            hash.Add("Dept", info.Dept);

			return hash;
		}

    }
}