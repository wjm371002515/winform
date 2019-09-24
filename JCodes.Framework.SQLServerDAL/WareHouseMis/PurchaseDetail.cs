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

namespace JCodes.Framework.SQLServerDAL
{
	/// <summary>
	/// PurchaseDetail 的摘要说明。
	/// </summary>
    public class PurchaseDetail : BaseDALSQLServer<PurchaseDetailInfo>, IPurchaseDetail
	{
		#region 对象实例及构造函数

		public static PurchaseDetail Instance
		{
			get
			{
				return new PurchaseDetail();
			}
		}
        public PurchaseDetail()
            : base(SQLServerPortal.gc._wareHouseTablePre + "PurchaseDetail", "Id")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override PurchaseDetailInfo DataReaderToEntity(IDataReader dataReader)
		{
			PurchaseDetailInfo purchaseDetailInfo = new PurchaseDetailInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			/*purchaseDetailInfo.ID = reader.GetInt32("ID");
			purchaseDetailInfo.PurchaseHead_ID = reader.GetInt32("PurchaseHead_ID");
			purchaseDetailInfo.OperationType = reader.GetString("OperationType");
			purchaseDetailInfo.ItemNo = reader.GetString("ItemNo");
			purchaseDetailInfo.ItemName = reader.GetString("ItemName");
			purchaseDetailInfo.MapNo = reader.GetString("MapNo");
			purchaseDetailInfo.Specification = reader.GetString("Specification");
			purchaseDetailInfo.Material = reader.GetString("Material");
			purchaseDetailInfo.ItemBigType = reader.GetString("ItemBigType");
			purchaseDetailInfo.ItemType = reader.GetString("ItemType");
			purchaseDetailInfo.Unit = reader.GetString("Unit");
			purchaseDetailInfo.Price = reader.GetDecimal("Price");
			purchaseDetailInfo.Quantity = reader.GetDouble("Quantity");
			purchaseDetailInfo.Amount = reader.GetDecimal("Amount");
			purchaseDetailInfo.Source = reader.GetString("Source");
			purchaseDetailInfo.StoragePos = reader.GetString("StoragePos");
			purchaseDetailInfo.UsagePos = reader.GetString("UsagePos");
            purchaseDetailInfo.WareHouse = reader.GetString("WareHouse");
            purchaseDetailInfo.Dept = reader.GetString("Dept");*/

			return purchaseDetailInfo;
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
			
 			/*hash.Add("PurchaseHead_ID", info.PurchaseHead_ID);
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
            */
			return hash;
		}

        public DataTable GetPurchaseDetailReportByID(int PurchaseHead_ID)
        {
            string sql = string.Format(@"Select UserCode,ItemNo,ItemName,MapNo,Specification,Material,
            ItemBigType,ItemType,Unit,Price,Quantity,Amount,Source,StoragePos,UsagePos,d.WareHouse,d.Dept
            From {0}PurchaseDetail d inner join {0}PurchaseHeader h on d.PurchaseHead_ID = h.ID 
            Where h.ID={1} order by CreateDate ", SQLServerPortal.gc._wareHouseTablePre, PurchaseHead_ID);
            return this.SqlTable(sql);
        }

    }
}