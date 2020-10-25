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
	/// 对象号: 000012
	/// LPR数据(LPRData)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2020-03-06 15:38:18.627
	/// </summary>
	public partial class LPRData : BaseDALSQLServer<LPRDataInfo>, ILPRData
	{
		#region 对象实例及构造函数
		public static LPRData Instance
		{
			get
			{
				return new LPRData();
			}
		}

		public LPRData() : base(SQLServerPortal.gc._dicTablePre + "LPRData", "")
		{
			this.sortField = "";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override LPRDataInfo DataReaderToEntity(IDataReader dataReader)
		{
			LPRDataInfo info = new LPRDataInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Nationality = reader.GetInt32("Nationality"); 	 //国籍
			info.Year = reader.GetInt32("Year"); 	 //记录年
			info.Month = reader.GetInt16("Month"); 	 //记录月
			info.Day = reader.GetInt16("Day"); 	 //记录日
			info.FloatValue = reader.GetDouble("FloatValue"); 	 //浮点型值
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(LPRDataInfo obj)
		{
			LPRDataInfo info = obj as LPRDataInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Nationality", info.Nationality); 	 //国籍
			hash.Add("Year", info.Year); 	 //记录年
			hash.Add("Month", info.Month); 	 //记录月
			hash.Add("Day", info.Day); 	 //记录日
			hash.Add("FloatValue", info.FloatValue); 	 //浮点型值
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
			dict.Add("Nationality", "国籍");
			dict.Add("Year", "记录年");
			dict.Add("Month", "记录月");
			dict.Add("Day", "记录日");
			dict.Add("FloatValue", "浮点型值");
			#endregion
			return dict;
		}
	}
}