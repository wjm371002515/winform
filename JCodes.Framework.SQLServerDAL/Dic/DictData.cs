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
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.SQLServerDAL
{
	/// <summary>
	/// 对象号: 000002
	/// 数据字典明细(DictData)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2018-02-08 13:22:39.518
	/// </summary>
	public partial class DictData : BaseDALSQLServer<DictDataInfo>, IDictData
	{
		#region 对象实例及构造函数
		public static DictData Instance
		{
			get
			{
				return new DictData();
			}
		}

		public DictData() : base(SQLServerPortal.gc._dicTablePre + "DictData", "Gid")
		{
			this.sortField = "Seq";
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override DictDataInfo DataReaderToEntity(IDataReader dataReader)
		{
			DictDataInfo info = new DictDataInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Gid = reader.GetString("Gid"); 	 //GUID对应的ID序号
			info.DicttypeId = reader.GetInt32("DicttypeId"); 	 //字典类型对应的ID
			info.DicttypeValue = reader.GetInt32("DicttypeValue"); 	 //字典键
			info.Name = reader.GetString("Name"); 	 //名称
			info.Remark = reader.GetString("Remark"); 	 //备注
			info.Seq = reader.GetString("Seq"); 	 //排序
			info.EditorId = reader.GetInt32("EditorId"); 	 //编辑人编号
			info.LastUpdateTime = reader.GetDateTime("LastUpdateTime"); 	 //最后更新时间
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(DictDataInfo obj)
		{
			DictDataInfo info = obj as DictDataInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Gid", info.Gid); 	 //GUID对应的ID序号
			hash.Add("DicttypeId", info.DicttypeId); 	 //字典类型对应的ID
			hash.Add("DicttypeValue", info.DicttypeValue); 	 //字典键
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("Remark", info.Remark); 	 //备注
			hash.Add("Seq", info.Seq); 	 //排序
			hash.Add("EditorId", info.EditorId); 	 //编辑人编号
			hash.Add("LastUpdateTime", info.LastUpdateTime); 	 //最后更新时间
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
			dict.Add("Gid", "GUID对应的ID序号");
			dict.Add("DicttypeId", "字典类型对应的ID");
			dict.Add("DicttypeValue", "字典键");
			dict.Add("Name", "名称");
			dict.Add("Remark", "备注");
			dict.Add("Seq", "排序");
			dict.Add("EditorId", "编辑人编号");
			dict.Add("LastUpdateTime", "最后更新时间");
			#endregion
			return dict;
		}
		
		 /// <summary>
        /// 根据字典类型ID获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictTypeId"></param>
        /// <returns></returns>
        public List<DictDataInfo> FindByTypeId(Int32 dicttypeId)
        {
            string condition = string.Format("DicttypeId={0} ", dicttypeId);
            return Find(condition);
        }

        private List<DicKeyValueInfo> GetDictBySql(string sql)
        {
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);

            List<DicKeyValueInfo> list = new List<DicKeyValueInfo>();
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Int32 dicttypeId = dr["DicttypeId"].ToString().ToInt32();
                    Int32 dicttypeValue = dr["DicttypeValue"].ToString().ToInt32();
                    string name = dr["Name"].ToString();
                    list.Add(new DicKeyValueInfo() { DicttypeId = dicttypeId, DicttypeValue = dicttypeValue, Name = name });
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所有的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <returns></returns>
        public List<DicKeyValueInfo> GetAllDict()
        {
            string sql = string.Format("select d.DicttypeId,d.Name,d.DicttypeValue from {0}DictData d inner join {0}DictType t on d.DicttypeId = t.Id order by d.DicttypeId, d.{1} {2}",
                SQLServerPortal.gc._dicTablePre, sortField, IsDescending ? "ASC" : "DESC");

            return GetDictBySql(sql);
        }
    }
}