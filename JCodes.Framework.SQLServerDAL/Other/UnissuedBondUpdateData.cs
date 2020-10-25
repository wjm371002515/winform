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
	/// 对象号: 000804
	/// 已取得批文未发行债券项目更新状态数据(UnissuedBondUpdateData)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2020-08-15 18:19:29.365
	/// </summary>
	public partial class UnissuedBondUpdateData : BaseDALSQLServer<UnissuedBondUpdateDataInfo>, IUnissuedBondUpdateData
	{
		#region 对象实例及构造函数
		public static UnissuedBondUpdateData Instance
		{
			get
			{
				return new UnissuedBondUpdateData();
			}
		}

        public UnissuedBondUpdateData()
            : base(SQLServerPortal.gc._otherTablePre + "UnissuedBondUpdateData", "Id")
		{
            this.sortField = "Id";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override UnissuedBondUpdateDataInfo DataReaderToEntity(IDataReader dataReader)
		{
			UnissuedBondUpdateDataInfo info = new UnissuedBondUpdateDataInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.DisplayName = reader.GetString("DisplayName"); 	 //显示名称
			info.Date = reader.GetInt32("Date"); 	 //年月日
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(UnissuedBondUpdateDataInfo obj)
		{
			UnissuedBondUpdateDataInfo info = obj as UnissuedBondUpdateDataInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("DisplayName", info.DisplayName); 	 //显示名称
			hash.Add("Date", info.Date); 	 //年月日
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
			dict.Add("DisplayName", "显示名称");
			dict.Add("Date", "年月日");
			#endregion
			return dict;
		}

        /// <summary>
        /// 获取DisplayName的值
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetDisplayNames(Int32 Id) { 
            string sql = string.Format(@"select STUFF((Select','+Convert(varchar(50), DisplayName ) FROM {0}UnissuedBondUpdateData where Id={1} FOR XML PATH('')),1,1,'') as {1}", SQLServerPortal.gc._otherTablePre, Id);
            return SqlValueList(sql);
        }
	}
}