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
	/// ReportMonthlyDetail 的摘要说明。
	/// </summary>
	public class ReportMonthlyDetail : BaseDALOracle<ReportMonthlyDetailInfo>, IReportMonthlyDetail
	{
		#region 对象实例及构造函数

		public static ReportMonthlyDetail Instance
		{
			get
			{
				return new ReportMonthlyDetail();
			}
		}
		public ReportMonthlyDetail() : base("WM_ReportMonthlyDetail","ID")
        {
            this.SeqName = string.Format("SEQ_{0}", tableName);//数值型主键，通过序列生成
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override ReportMonthlyDetailInfo DataReaderToEntity(IDataReader dataReader)
		{
			ReportMonthlyDetailInfo info = new ReportMonthlyDetailInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetInt32("ID");
			info.Header_ID = reader.GetInt32("Header_ID");
			info.ReportYear = reader.GetInt32("ReportYear");
			info.ReportMonth = reader.GetInt32("ReportMonth");
			info.YearMonth = reader.GetString("YearMonth");
			info.ItemName = reader.GetString("ItemName");
			info.LastCount = reader.GetInt32("LastCount");
			info.LastMoney = reader.GetDecimal("LastMoney");
			info.CurrentInCount = reader.GetInt32("CurrentInCount");
			info.CurrentInMoney = reader.GetDecimal("CurrentInMoney");
			info.CurrentOutCount = reader.GetInt32("CurrentOutCount");
			info.CurrentOutMoney = reader.GetDecimal("CurrentOutMoney");
			info.CurrentCount = reader.GetInt32("CurrentCount");
			info.CurrentMoney = reader.GetDecimal("CurrentMoney");
            info.ReportCode = reader.GetString("ReportCode");
			
			return info;
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

            hash.Add("ID", info.ID);
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