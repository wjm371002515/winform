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
	/// ReportAnnualCostHeader 的摘要说明。
	/// </summary>
	public class ReportAnnualCostHeader : BaseDALOracle<ReportAnnualCostHeaderInfo>, IReportAnnualCostHeader
	{
		#region 对象实例及构造函数

		public static ReportAnnualCostHeader Instance
		{
			get
			{
				return new ReportAnnualCostHeader();
			}
		}
		public ReportAnnualCostHeader() : base("WM_ReportAnnualCostHeader","ID")
        {
            this.SeqName = string.Format("SEQ_{0}", tableName);//数值型主键，通过序列生成
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override ReportAnnualCostHeaderInfo DataReaderToEntity(IDataReader dataReader)
		{
			ReportAnnualCostHeaderInfo info = new ReportAnnualCostHeaderInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetInt32("ID");
			info.ReportType = reader.GetInt32("ReportType");
			info.ReportTitle = reader.GetString("ReportTitle");
			info.ReportYear = reader.GetInt32("ReportYear");
			info.CreateDate = reader.GetDateTime("CreateDate");
			info.Creator = reader.GetString("Creator");
			info.Note = reader.GetString("Note");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(ReportAnnualCostHeaderInfo obj)
		{
		    ReportAnnualCostHeaderInfo info = obj as ReportAnnualCostHeaderInfo;
			Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
 			hash.Add("ReportType", info.ReportType);
 			hash.Add("ReportTitle", info.ReportTitle);
 			hash.Add("ReportYear", info.ReportYear);
 			hash.Add("CreateDate", info.CreateDate);
 			hash.Add("Creator", info.Creator);
 			hash.Add("Note", info.Note);
 				
			return hash;
		}

    }
}