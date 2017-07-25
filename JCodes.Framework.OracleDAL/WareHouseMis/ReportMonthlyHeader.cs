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

namespace JCodes.Framework.OracleDAL
{
	/// <summary>
	/// ReportMonthlyHeader 的摘要说明。
	/// </summary>
	public class ReportMonthlyHeader : BaseDALOracle<ReportMonthlyHeaderInfo>, IReportMonthlyHeader
	{
		#region 对象实例及构造函数

		public static ReportMonthlyHeader Instance
		{
			get
			{
				return new ReportMonthlyHeader();
			}
		}
        public ReportMonthlyHeader()
            : base(OraclePortal.gc._wareHouseTablePre + "ReportMonthlyHeader", "ID")
        {
            this.SeqName = string.Format("SEQ_{0}", tableName);//数值型主键，通过序列生成
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override ReportMonthlyHeaderInfo DataReaderToEntity(IDataReader dataReader)
		{
			ReportMonthlyHeaderInfo info = new ReportMonthlyHeaderInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetInt32("ID");
			info.ReportType = reader.GetInt32("ReportType");
			info.ReportTitle = reader.GetString("ReportTitle");
			info.ReportYear = reader.GetInt32("ReportYear");
			info.ReportMonth = reader.GetInt32("ReportMonth");
			info.YearMonth = reader.GetString("YearMonth");
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
        protected override Hashtable GetHashByEntity(ReportMonthlyHeaderInfo obj)
		{
		    ReportMonthlyHeaderInfo info = obj as ReportMonthlyHeaderInfo;
			Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
 			hash.Add("ReportType", info.ReportType);
 			hash.Add("ReportTitle", info.ReportTitle);
 			hash.Add("ReportYear", info.ReportYear);
 			hash.Add("ReportMonth", info.ReportMonth);
 			hash.Add("YearMonth", info.YearMonth);
 			hash.Add("CreateDate", info.CreateDate);
 			hash.Add("Creator", info.Creator);
 			hash.Add("Note", info.Note);
 				
			return hash;
		}

    }
}