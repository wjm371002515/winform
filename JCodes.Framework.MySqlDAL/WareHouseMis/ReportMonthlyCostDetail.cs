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
	/// ReportMonthlyCostDetail 的摘要说明。
	/// </summary>
	public class ReportMonthlyCostDetail : BaseDALMySql<ReportMonthlyCostDetailInfo>, IReportMonthlyCostDetail
	{
		#region 对象实例及构造函数

		public static ReportMonthlyCostDetail Instance
		{
			get
			{
				return new ReportMonthlyCostDetail();
			}
		}
        public ReportMonthlyCostDetail()
            : base(MySqlPortal.gc._wareHouseTablePre + "ReportMonthlyCostDetail", "ID")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override ReportMonthlyCostDetailInfo DataReaderToEntity(IDataReader dataReader)
		{
			ReportMonthlyCostDetailInfo reportMonthlyCostDetailInfo = new ReportMonthlyCostDetailInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			reportMonthlyCostDetailInfo.ID = reader.GetInt32("ID");
			reportMonthlyCostDetailInfo.Header_ID = reader.GetInt32("Header_ID");
			reportMonthlyCostDetailInfo.ReportYear = reader.GetInt32("ReportYear");
			reportMonthlyCostDetailInfo.ReportMonth = reader.GetInt32("ReportMonth");
			reportMonthlyCostDetailInfo.YearMonth = reader.GetString("YearMonth");
			reportMonthlyCostDetailInfo.DeptName = reader.GetString("DeptName");
			reportMonthlyCostDetailInfo.ItemType = reader.GetString("ItemType");
			reportMonthlyCostDetailInfo.TotalMoney = reader.GetDecimal("TotalMoney");
			reportMonthlyCostDetailInfo.ReportCode = reader.GetString("ReportCode");
			
			return reportMonthlyCostDetailInfo;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(ReportMonthlyCostDetailInfo obj)
		{
		    ReportMonthlyCostDetailInfo info = obj as ReportMonthlyCostDetailInfo;
			Hashtable hash = new Hashtable(); 
			
 			hash.Add("Header_ID", info.Header_ID);
 			hash.Add("ReportYear", info.ReportYear);
 			hash.Add("ReportMonth", info.ReportMonth);
 			hash.Add("YearMonth", info.YearMonth);
 			hash.Add("DeptName", info.DeptName);
 			hash.Add("ItemType", info.ItemType);
 			hash.Add("TotalMoney", info.TotalMoney);
 			hash.Add("ReportCode", info.ReportCode);
 				
			return hash;
		}

    }
}