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

namespace JCodes.Framework.SQLiteDAL
{
	/// <summary>
	/// ReportMonthlyDetail 的摘要说明。
	/// </summary>
	public class ReportMonthlyDetail : BaseDALSQLite<ReportMonthlyDetailInfo>, IReportMonthlyDetail
	{
		#region 对象实例及构造函数

		public static ReportMonthlyDetail Instance
		{
			get
			{
				return new ReportMonthlyDetail();
			}
		}
        public ReportMonthlyDetail()
            : base(SQLitePortal.gc._wareHouseTablePre + "ReportMonthlyDetail", "ID")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override ReportMonthlyDetailInfo DataReaderToEntity(IDataReader dataReader)
		{
			ReportMonthlyDetailInfo reportMonthlyDetailInfo = new ReportMonthlyDetailInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			reportMonthlyDetailInfo.ID = reader.GetInt32("ID");
			reportMonthlyDetailInfo.Header_ID = reader.GetInt32("Header_ID");
			reportMonthlyDetailInfo.ReportYear = reader.GetInt32("ReportYear");
			reportMonthlyDetailInfo.ReportMonth = reader.GetInt32("ReportMonth");
			reportMonthlyDetailInfo.YearMonth = reader.GetString("YearMonth");
			reportMonthlyDetailInfo.ItemName = reader.GetString("ItemName");
			reportMonthlyDetailInfo.LastCount = reader.GetInt32("LastCount");
			reportMonthlyDetailInfo.LastMoney = reader.GetDecimal("LastMoney");
			reportMonthlyDetailInfo.CurrentInCount = reader.GetInt32("CurrentInCount");
			reportMonthlyDetailInfo.CurrentInMoney = reader.GetDecimal("CurrentInMoney");
			reportMonthlyDetailInfo.CurrentOutCount = reader.GetInt32("CurrentOutCount");
			reportMonthlyDetailInfo.CurrentOutMoney = reader.GetDecimal("CurrentOutMoney");
			reportMonthlyDetailInfo.CurrentCount = reader.GetInt32("CurrentCount");
			reportMonthlyDetailInfo.CurrentMoney = reader.GetDecimal("CurrentMoney");
            reportMonthlyDetailInfo.ReportCode = reader.GetString("ReportCode");
			
			return reportMonthlyDetailInfo;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(ReportMonthlyDetailInfo obj)
		{
		    ReportMonthlyDetailInfo info = obj as ReportMonthlyDetailInfo;
			Hashtable hash = new Hashtable(); 
			
 			hash.Add("Header_ID", info.Header_ID);
 			hash.Add("ReportYear", info.ReportYear);
 			hash.Add("ReportMonth", info.ReportMonth);
 			hash.Add("YearMonth", info.YearMonth);
 			hash.Add("ItemName", info.ItemName);
 			hash.Add("LastCount", info.LastCount);
 			hash.Add("LastMoney", info.LastMoney);
 			hash.Add("CurrentInCount", info.CurrentInCount);
 			hash.Add("CurrentInMoney", info.CurrentInMoney);
 			hash.Add("CurrentOutCount", info.CurrentOutCount);
 			hash.Add("CurrentOutMoney", info.CurrentOutMoney);
 			hash.Add("CurrentCount", info.CurrentCount);
 			hash.Add("CurrentMoney", info.CurrentMoney);
            hash.Add("ReportCode", info.ReportCode);
 				
			return hash;
		}

    }
}