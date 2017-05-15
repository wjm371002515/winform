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
	/// ItemDetail 的摘要说明。
	/// </summary>
	public class ItemDetail : BaseDALOracle<ItemDetailInfo>, IItemDetail
	{
		#region 对象实例及构造函数

		public static ItemDetail Instance
		{
			get
			{
				return new ItemDetail();
			}
		}
		public ItemDetail() : base("WM_ItemDetail","ID")
        {
            this.SeqName = string.Format("SEQ_{0}", tableName);//数值型主键，通过序列生成
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override ItemDetailInfo DataReaderToEntity(IDataReader dataReader)
		{
			ItemDetailInfo info = new ItemDetailInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetInt32("ID");
			info.ItemNo = reader.GetString("ItemNo");
			info.ItemName = reader.GetString("ItemName");
            info.Manufacture = reader.GetString("Manufacture");
			info.MapNo = reader.GetString("MapNo");
            info.Specification = reader.GetString("Specification");
			info.Material = reader.GetString("Material");
			info.ItemBigType = reader.GetString("ItemBigType");
			info.ItemType = reader.GetString("ItemType");
			info.Unit = reader.GetString("Unit");
			info.Price = reader.GetDecimal("Price");
			info.Source = reader.GetString("Source");
			info.StoragePos = reader.GetString("StoragePos");
			info.UsagePos = reader.GetString("UsagePos");
			info.Note = reader.GetString("Note");
            info.WareHouse = reader.GetString("WareHouse");
            info.Dept = reader.GetString("Dept");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(ItemDetailInfo obj)
		{
		    ItemDetailInfo info = obj as ItemDetailInfo;
			Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
 			hash.Add("ItemNo", info.ItemNo);
 			hash.Add("ItemName", info.ItemName);
            hash.Add("Manufacture", info.Manufacture);
 			hash.Add("MapNo", info.MapNo);
 			hash.Add("Specification", info.Specification);
 			hash.Add("Material", info.Material);
 			hash.Add("ItemBigType", info.ItemBigType);
 			hash.Add("ItemType", info.ItemType);
 			hash.Add("Unit", info.Unit);
 			hash.Add("Price", info.Price);
 			hash.Add("Source", info.Source);
 			hash.Add("StoragePos", info.StoragePos);
 			hash.Add("UsagePos", info.UsagePos);
 			hash.Add("Note", info.Note);
            hash.Add("WareHouse", info.WareHouse);
            hash.Add("Dept", info.Dept);
 				
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

            dict.Add("ID", "编号");
            dict.Add("ItemNo", "项目编号");
            dict.Add("ItemName", "项目名称");
            dict.Add("Manufacture", "供货商");
            dict.Add("MapNo", "图号");
            dict.Add("Specification", "规格型号");
            dict.Add("Material", "材质");
            dict.Add("ItemBigType", "备件属类");
            dict.Add("ItemType", "备件类别");
            dict.Add("Unit", "单位");
            dict.Add("Price", "单价");
            dict.Add("Source", "来源");
            dict.Add("StoragePos", "库位");
            dict.Add("UsagePos", "使用位置");
            dict.Add("StockQuantity", "当前库存");
            dict.Add("WareHouse", "所属库房");
            dict.Add("Dept", "所属部门");
            dict.Add("Note", "备注");

            #endregion
            return dict;
        }

        #region IItemDetail 成员

        /// <summary>
        /// 根据备件属类获取该类型的备件列表
        /// </summary>
        /// <param name="bigType">备件属类</param>
        /// <returns></returns>
        public List<ItemDetailInfo> FindByBigType(string bigType)
        {
            string sql = string.Format("ItemBigType like '%{0}%' ", bigType);
            return this.Find(sql);
        }

        /// <summary>
        /// 根据备件类型获取该类型的备件列表
        /// </summary>
        /// <param name="itemType">备件类型</param>
        /// <returns></returns>
        public List<ItemDetailInfo> FindByItemType(string itemType, string wareHouse)
        {
            string sql = string.Format("(ItemType like '%{0}%') AND WareHouse ='{1}' ", itemType, wareHouse);
            return this.Find(sql);
        }

        /// <summary>
        /// 根据备件名称获取列表
        /// </summary>
        /// <param name="itemName">备件名称</param>
        /// <returns></returns>
        public List<ItemDetailInfo> FindByName(string itemName)
        {
            string sql = string.Format("ItemName like '%{0}%' ", itemName);
            return this.Find(sql);
        }
                 
        /// <summary>
        /// 根据备件编码获取列表
        /// </summary>
        /// <param name="itemNo">备件类型</param>
        /// <returns></returns>
        public ItemDetailInfo FindByItemNo(string itemNo)
        {
            string sql = string.Format("Select {0} From {1} Where (ItemNo = '{2}')", selectedFields, tableName, itemNo);

            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);
            ItemDetailInfo entity = null;
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    entity = DataReaderToEntity(dr);
                }
            }
            return entity;
        }

        /// <summary>
        /// 根据备件名称和备件编号获取列表
        /// </summary>
        /// <param name="itemName">备件名称</param>
        /// <param name="itemNo">备件编码</param>
        /// <returns></returns>
        public List<ItemDetailInfo> FindByNameAndNo(string itemName, string itemNo, string wareHouse)
        {
            string sql = string.Format("(ItemName like '%{0}%') AND WareHouse='{1}' ", itemName, wareHouse);
            if (!string.IsNullOrEmpty(itemNo))
            {
                sql += string.Format(" AND (ItemNo like '{0}%') ", itemNo);  
            }
            return this.Find(sql);
        }

        #endregion
    }
}