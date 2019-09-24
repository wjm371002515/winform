using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 年费用成本明细报表信息(ReportAnnualCostDetailInfo)
	/// 对象号: 100074
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class ReportAnnualCostDetailInfo : BaseEntity
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
		/// 备件类别
		/// </summary>
		private Int32 m_ItemType = 0;

		/// <summary>
		/// 成本中心
		/// </summary>
		private String m_CostCenter = string.Empty;

		/// <summary>
		/// 报表代码
		/// </summary>
		private String m_ReportCode = string.Empty;

		/// <summary>
		/// 总金额1
		/// </summary>
		private decimal m_One;

		/// <summary>
		/// 总金额2
		/// </summary>
		private decimal m_Two;

		/// <summary>
		/// 总金额3
		/// </summary>
		private decimal m_Three;

		/// <summary>
		/// 总金额4
		/// </summary>
		private decimal m_Four;

		/// <summary>
		/// 总金额5
		/// </summary>
		private decimal m_Five;

		/// <summary>
		/// 总金额6
		/// </summary>
		private decimal m_Six;

		/// <summary>
		/// 总金额7
		/// </summary>
		private decimal m_Seven;

		/// <summary>
		/// 总金额8
		/// </summary>
		private decimal m_Eight;

		/// <summary>
		/// 总金额9
		/// </summary>
		private decimal m_Nine;

		/// <summary>
		/// 总金额10
		/// </summary>
		private decimal m_Ten;

		/// <summary>
		/// 总金额11
		/// </summary>
		private decimal m_Eleven;

		/// <summary>
		/// 总金额12
		/// </summary>
		private decimal m_Twelve;

		/// <summary>
		/// 总金额
		/// </summary>
		private decimal m_Total;
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
		/// 成本中心
		/// </summary>
		[DataMember]
		[DisplayName("成本中心")]
		public virtual String CostCenter
		{
			get
			{
				return this.m_CostCenter;
			}
			set
			{
				this.m_CostCenter = value;
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
		/// 总金额1
		/// </summary>
		[DataMember]
		public virtual decimal One
		{
			get
			{
				return this.m_One;
			}
			set
			{
				this.m_One = value;
			}
		}

		/// <summary>
		/// 总金额2
		/// </summary>
		[DataMember]
		public virtual decimal Two
		{
			get
			{
				return this.m_Two;
			}
			set
			{
				this.m_Two = value;
			}
		}

		/// <summary>
		/// 总金额3
		/// </summary>
		[DataMember]
		public virtual decimal Three
		{
			get
			{
				return this.m_Three;
			}
			set
			{
				this.m_Three = value;
			}
		}

		/// <summary>
		/// 总金额4
		/// </summary>
		[DataMember]
		public virtual decimal Four
		{
			get
			{
				return this.m_Four;
			}
			set
			{
				this.m_Four = value;
			}
		}

		/// <summary>
		/// 总金额5
		/// </summary>
		[DataMember]
		public virtual decimal Five
		{
			get
			{
				return this.m_Five;
			}
			set
			{
				this.m_Five = value;
			}
		}

		/// <summary>
		/// 总金额6
		/// </summary>
		[DataMember]
		public virtual decimal Six
		{
			get
			{
				return this.m_Six;
			}
			set
			{
				this.m_Six = value;
			}
		}

		/// <summary>
		/// 总金额7
		/// </summary>
		[DataMember]
		public virtual decimal Seven
		{
			get
			{
				return this.m_Seven;
			}
			set
			{
				this.m_Seven = value;
			}
		}

		/// <summary>
		/// 总金额8
		/// </summary>
		[DataMember]
		public virtual decimal Eight
		{
			get
			{
				return this.m_Eight;
			}
			set
			{
				this.m_Eight = value;
			}
		}

		/// <summary>
		/// 总金额9
		/// </summary>
		[DataMember]
		public virtual decimal Nine
		{
			get
			{
				return this.m_Nine;
			}
			set
			{
				this.m_Nine = value;
			}
		}

		/// <summary>
		/// 总金额10
		/// </summary>
		[DataMember]
		public virtual decimal Ten
		{
			get
			{
				return this.m_Ten;
			}
			set
			{
				this.m_Ten = value;
			}
		}

		/// <summary>
		/// 总金额11
		/// </summary>
		[DataMember]
		public virtual decimal Eleven
		{
			get
			{
				return this.m_Eleven;
			}
			set
			{
				this.m_Eleven = value;
			}
		}

		/// <summary>
		/// 总金额12
		/// </summary>
		[DataMember]
		public virtual decimal Twelve
		{
			get
			{
				return this.m_Twelve;
			}
			set
			{
				this.m_Twelve = value;
			}
		}

		/// <summary>
		/// 总金额
		/// </summary>
		[DataMember]
		public virtual decimal Total
		{
			get
			{
				return this.m_Total;
			}
			set
			{
				this.m_Total = value;
			}
		}
		#endregion
	}
}