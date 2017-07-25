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

namespace JCodes.Framework.MySqlDAL
{
	/// <summary>
	/// ReportAnnualCostHeader 的摘要说明。
	/// </summary>
	public class ReportAnnualCostHeader : BaseDALMySql<ReportAnnualCostHeaderInfo>, IReportAnnualCostHeader
	{
		#region 对象实例及构造函数

		public static ReportAnnualCostHeader Instance
		{
			get
			{
				return new ReportAnnualCostHeader();
			}
		}
        public ReportAnnualCostHeader()
            : base(MySqlPortal.gc._wareHouseTablePre + "ReportAnnualCostHeader", "ID")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override ReportAnnualCostHeaderInfo DataReaderToEntity(IDataReader dataReader)
		{
			ReportAnnualCostHeaderInfo reportAnnualCostHeaderInfo = new ReportAnnualCostHeaderInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			reportAnnualCostHeaderInfo.ID = reader.GetInt32("ID");
			reportAnnualCostHeaderInfo.ReportType = reader.GetInt32("ReportType");
			reportAnnualCostHeaderInfo.ReportTitle = reader.GetString("ReportTitle");
			reportAnnualCostHeaderInfo.ReportYear = reader.GetInt32("ReportYear");
			reportAnnualCostHeaderInfo.CreateDate = reader.GetDateTime("CreateDate");
			reportAnnualCostHeaderInfo.Creator = reader.GetString("Creator");
			reportAnnualCostHeaderInfo.Note = reader.GetString("Note");
			
			return reportAnnualCostHeaderInfo;
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