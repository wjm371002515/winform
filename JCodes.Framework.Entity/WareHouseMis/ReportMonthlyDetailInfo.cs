using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 月费用清单明细信息(ReportMonthlyDetailInfo)
	/// 对象号: 100077
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class ReportMonthlyDetailInfo : BaseEntity
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
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 报表代码
		/// </summary>
		private String m_ReportCode = string.Empty;

		/// <summary>
		/// 上月结存数量
		/// </summary>
		private Int32 m_LastCount = 0;

		/// <summary>
		/// 上月结存金额
		/// </summary>
		private decimal m_LastMoney;

		/// <summary>
		/// 本月入库数量
		/// </summary>
		private Int32 m_CurrentInCount = 0;

		/// <summary>
		/// 本月入库金额
		/// </summary>
		private decimal m_CurrentInMoney;

		/// <summary>
		/// 本月出库数量
		/// </summary>
		private Int32 m_CurrentOutCount = 0;

		/// <summary>
		/// 本月出库金额
		/// </summary>
		private decimal m_CurrentOutMoney;

		/// <summary>
		/// 本月结存金额
		/// </summary>
		private Int32 m_CurrentCount = 0;

		/// <summary>
		/// 本月结存金额
		/// </summary>
		private decimal m_CurrentMoney;
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
		/// 名称
		/// </summary>
		[DataMember]
		[DisplayName("名称")]
		public virtual String Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
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

		/// <summary>
		/// 上月结存数量
		/// </summary>
		[DataMember]
		public virtual Int32 LastCount
		{
			get
			{
				return this.m_LastCount;
			}
			set
			{
				this.m_LastCount = value;
			}
		}

		/// <summary>
		/// 上月结存金额
		/// </summary>
		[DataMember]
		public virtual decimal LastMoney
		{
			get
			{
				return this.m_LastMoney;
			}
			set
			{
				this.m_LastMoney = value;
			}
		}

		/// <summary>
		/// 本月入库数量
		/// </summary>
		[DataMember]
		public virtual Int32 CurrentInCount
		{
			get
			{
				return this.m_CurrentInCount;
			}
			set
			{
				this.m_CurrentInCount = value;
			}
		}

		/// <summary>
		/// 本月入库金额
		/// </summary>
		[DataMember]
		public virtual decimal CurrentInMoney
		{
			get
			{
				return this.m_CurrentInMoney;
			}
			set
			{
				this.m_CurrentInMoney = value;
			}
		}

		/// <summary>
		/// 本月出库数量
		/// </summary>
		[DataMember]
		public virtual Int32 CurrentOutCount
		{
			get
			{
				return this.m_CurrentOutCount;
			}
			set
			{
				this.m_CurrentOutCount = value;
			}
		}

		/// <summary>
		/// 本月出库金额
		/// </summary>
		[DataMember]
		public virtual decimal CurrentOutMoney
		{
			get
			{
				return this.m_CurrentOutMoney;
			}
			set
			{
				this.m_CurrentOutMoney = value;
			}
		}

		/// <summary>
		/// 本月结存金额
		/// </summary>
		[DataMember]
		public virtual Int32 CurrentCount
		{
			get
			{
				return this.m_CurrentCount;
			}
			set
			{
				this.m_CurrentCount = value;
			}
		}

		/// <summary>
		/// 本月结存金额
		/// </summary>
		[DataMember]
		public virtual decimal CurrentMoney
		{
			get
			{
				return this.m_CurrentMoney;
			}
			set
			{
				this.m_CurrentMoney = value;
			}
		}
		#endregion
	}
}