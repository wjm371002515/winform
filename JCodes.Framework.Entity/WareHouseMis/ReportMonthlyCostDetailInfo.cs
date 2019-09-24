using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 月费用成本明细报表信息(ReportMonthlyCostDetailInfo)
	/// 对象号: 100076
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class ReportMonthlyCostDetailInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 报表头编号
		/// </summary>
		private Int32 m_ReportHeaderId = 0;

		/// <summary>
		/// 报表年份
		/// </summary>
		private Int32 m_ReportYear = 0;

		/// <summary>
		/// 报表月份
		/// </summary>
		private Int16 m_ReportMonth = 0;

		/// <summary>
		/// 部门Id
		/// </summary>
		private Int32 m_DeptId = 0;

		/// <summary>
		/// 备件类别
		/// </summary>
		private Int32 m_ItemType = 0;

		/// <summary>
		/// 金额
		/// </summary>
		private Double m_Balance = 0.0;

		/// <summary>
		/// 报表代码
		/// </summary>
		private String m_ReportCode = string.Empty;
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
		/// 报表头编号
		/// </summary>
		[DataMember]
		[DisplayName("报表头编号")]
		public virtual Int32 ReportHeaderId
		{
			get
			{
				return this.m_ReportHeaderId;
			}
			set
			{
				this.m_ReportHeaderId = value;
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
		/// 部门Id
		/// </summary>
		[DataMember]
		[DisplayName("部门Id")]
		public virtual Int32 DeptId
		{
			get
			{
				return this.m_DeptId;
			}
			set
			{
				this.m_DeptId = value;
			}
		}

		/// <summary>
		/// 备件类别
		/// </summary>
		[DataMember]
		[DisplayName("备件类别")]
		public virtual Int32 ItemType
		{
			get
			{
				return this.m_ItemType;
			}
			set
			{
				this.m_ItemType = value;
			}
		}

		/// <summary>
		/// 金额
		/// </summary>
		[DataMember]
		[DisplayName("金额")]
		public virtual Double Balance
		{
			get
			{
				return this.m_Balance;
			}
			set
			{
				this.m_Balance = value;
			}
		}

		/// <summary>
		/// 报表代码
		/// </summary>
		[DataMember]
		[DisplayName("报表代码")]
		public virtual String ReportCode
		{
			get
			{
				return this.m_ReportCode;
			}
			set
			{
				this.m_ReportCode = value;
			}
		}
		#endregion
	}
}