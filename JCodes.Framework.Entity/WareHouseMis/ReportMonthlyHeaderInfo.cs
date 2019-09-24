using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 月费用表头报表信息(ReportMonthlyHeaderInfo)
	/// 对象号: 100078
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class ReportMonthlyHeaderInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 报表类型
		/// </summary>
		private Int16 m_ReportType = 0;

		/// <summary>
		/// 报表标题 
		/// </summary>
		private String m_ReportTitle = string.Empty;

		/// <summary>
		/// 报表年份
		/// </summary>
		private Int32 m_ReportYear = 0;

		/// <summary>
		/// 报表月份
		/// </summary>
		private Int16 m_ReportMonth = 0;

		/// <summary>
		/// 创建人编号
		/// </summary>
		private Int32 m_CreatorId = 0;

		/// <summary>
		/// 创建时间
		/// </summary>
		private DateTime m_CreatorTime = DateTime.Now;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;
		#endregion

		#region Property Members

		/// <summary>
		/// ID序号
		/// </summary>
		[DataMember]
		[DisplayName("ID序号")]
		public virtual Int32 Id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		/// <summary>
		/// 报表类型
		/// </summary>
		[DataMember]
		[DisplayName("报表类型")]
		public virtual Int16 ReportType
		{
			get
			{
				return this.m_ReportType;
			}
			set
			{
				this.m_ReportType = value;
			}
		}

		/// <summary>
		/// 报表标题 
		/// </summary>
		[DataMember]
		[DisplayName("报表标题 ")]
		public virtual String ReportTitle
		{
			get
			{
				return this.m_ReportTitle;
			}
			set
			{
				this.m_ReportTitle = value;
			}
		}

		/// <summary>
		/// 报表年份
		/// </summary>
		[DataMember]
		[DisplayName("报表年份")]
		public virtual Int32 ReportYear
		{
			get
			{
				return this.m_ReportYear;
			}
			set
			{
				this.m_ReportYear = value;
			}
		}

		/// <summary>
		/// 报表月份
		/// </summary>
		[DataMember]
		[DisplayName("报表月份")]
		public virtual Int16 ReportMonth
		{
			get
			{
				return this.m_ReportMonth;
			}
			set
			{
				this.m_ReportMonth = value;
			}
		}

		/// <summary>
		/// 创建人编号
		/// </summary>
		[DataMember]
		[DisplayName("创建人编号")]
		public virtual Int32 CreatorId
		{
			get
			{
				return this.m_CreatorId;
			}
			set
			{
				this.m_CreatorId = value;
			}
		}

		/// <summary>
		/// 创建时间
		/// </summary>
		[DataMember]
		[DisplayName("创建时间")]
		public virtual DateTime CreatorTime
		{
			get
			{
				return this.m_CreatorTime;
			}
			set
			{
				this.m_CreatorTime = value;
			}
		}

		/// <summary>
		/// 备注
		/// </summary>
		[DataMember]
		[DisplayName("备注")]
		public virtual String Remark
		{
			get
			{
				return this.m_Remark;
			}
			set
			{
				this.m_Remark = value;
			}
		}
		#endregion
	}
}