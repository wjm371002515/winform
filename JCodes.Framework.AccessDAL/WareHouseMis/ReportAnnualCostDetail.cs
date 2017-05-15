using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;

namespace JCodes.Framework.AccessDAL
{
	/// <summary>
	/// ReportAnnualCostDetail 的摘要说明。
	/// </summary>
	public class ReportAnnualCostDetail : BaseDALAccess<ReportAnnualCostDetailInfo>, IReportAnnualCostDetail
	{
		#region 对象实例及构造函数

		public static ReportAnnualCostDetail Instance
		{
			get
			{
				return new ReportAnnualCostDetail();
			}
		}
		public ReportAnnualCostDetail() : base("WM_ReportAnnualCostDetail","ID")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override ReportAnnualCostDetailInfo DataReaderToEntity(IDataReader dataReader)
		{
			ReportAnnualCostDetailInfo reportAnnualCostDetailInfo = new ReportAnnualCostDetailInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			reportAnnualCostDetailInfo.ID = reader.GetInt32("ID");
			reportAnnualCostDetailInfo.Header_ID = reader.GetInt32("Header_ID");
			reportAnnualCostDetailInfo.ReportYear = reader.GetInt32("ReportYear");
			reportAnnualCostDetailInfo.ItemType = reader.GetString("ItemType");
			reportAnnualCostDetailInfo.CostCenterOrDept = reader.GetString("CostCenterOrDept");
			reportAnnualCostDetailInfo.One = reader.GetDecimal("One");
			reportAnnualCostDetailInfo.Two = reader.GetDecimal("Two");
			reportAnnualCostDetailInfo.Three = reader.GetDecimal("Three");
			reportAnnualCostDetailInfo.Four = reader.GetDecimal("Four");
			reportAnnualCostDetailInfo.Five = reader.GetDecimal("Five");
			reportAnnualCostDetailInfo.Six = reader.GetDecimal("Six");
			reportAnnualCostDetailInfo.Seven = reader.GetDecimal("Seven");
			reportAnnualCostDetailInfo.Eight = reader.GetDecimal("Eight");
			reportAnnualCostDetailInfo.Nine = reader.GetDecimal("Nine");
			reportAnnualCostDetailInfo.Ten = reader.GetDecimal("Ten");
            reportAnnualCostDetailInfo.Eleven = reader.GetDecimal("Eleven");
            reportAnnualCostDetailInfo.Twelve = reader.GetDecimal("Twelve");
            reportAnnualCostDetailInfo.Total = reader.GetDecimal("Total");
			reportAnnualCostDetailInfo.ReportCode = reader.GetString("ReportCode");
			
			return reportAnnualCostDetailInfo;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(ReportAnnualCostDetailInfo obj)
		{
		    ReportAnnualCostDetailInfo info = obj as ReportAnnualCostDetailInfo;
			Hashtable hash = new Hashtable(); 
			
 			hash.Add("Header_ID", info.Header_ID);
 			hash.Add("ReportYear", info.ReportYear);
 			hash.Add("ItemType", info.ItemType);
 			hash.Add("CostCenterOrDept", info.CostCenterOrDept);
 			hash.Add("One", info.One);
 			hash.Add("Two", info.Two);
 			hash.Add("Three", info.Three);
 			hash.Add("Four", info.Four);
 			hash.Add("Five", info.Five);
 			hash.Add("Six", info.Six);
 			hash.Add("Seven", info.Seven);
 			hash.Add("Eight", info.Eight);
 			hash.Add("Nine", info.Nine);
 			hash.Add("Ten", info.Ten);
            hash.Add("Eleven", info.Eleven);
            hash.Add("Twelve", info.Twelve);
            hash.Add("Total", info.Total);
 			hash.Add("ReportCode", info.ReportCode);
 				
			return hash;
		}

    }
}