using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 进货单表头信息(PurchaseHeaderInfo)
	/// 对象号: 100073
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class PurchaseHeaderInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 订货单
		/// </summary>
		private String m_OrderNo = string.Empty;

		/// <summary>
		/// 操作类型
		/// </summary>
		private Int16 m_OperationType = 0;

		/// <summary>
		/// 供应商
		/// </summary>
		private String m_Manufacture = string.Empty;

		/// <summary>
		/// 库房编号
		/// </summary>
		private Int32 m_WareHouseId = 0;

		/// <summary>
		/// 成本中心
		/// </summary>
		private String m_CostCenter = string.Empty;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;

		/// <summary>
		/// 创建人编号
		/// </summary>
		private Int32 m_CreatorId = 0;

		/// <summary>
		/// 创建时间
		/// </summary>
		private DateTime m_CreatorTime = DateTime.Now;

		/// <summary>
		/// 创建年份
		/// </summary>
		private Int32 m_CreatorYear = 0;

		/// <summary>
		/// 创建月份
		/// </summary>
		private Int16 m_CreatorMonth = 0;

		/// <summary>
		/// 领料人
		/// </summary>
		private String m_PickingPeople = string.Empty;
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
		/// 订货单
		/// </summary>
		[DataMember]
		[DisplayName("订货单")]
		public virtual String OrderNo
		{
			get
			{
				return this.m_OrderNo;
			}
			set
			{
				this.m_OrderNo = value;
			}
		}

		/// <summary>
		/// 操作类型
		/// 1-新增,
		/// 2-修改,
		/// 3-删除,
		/// 4-查询
		/// </summary>
		[DataMember]
		[DisplayName("操作类型")]
		public virtual Int16 OperationType
		{
			get
			{
				return this.m_OperationType;
			}
			set
			{
				this.m_OperationType = value;
			}
		}

		/// <summary>
		/// 供应商
		/// </summary>
		[DataMember]
		[DisplayName("供应商")]
		public virtual String Manufacture
		{
			get
			{
				return this.m_Manufacture;
			}
			set
			{
				this.m_Manufacture = value;
			}
		}

		/// <summary>
		/// 库房编号
		/// </summary>
		[DataMember]
		[DisplayName("库房编号")]
		public virtual Int32 WareHouseId
		{
			get
			{
				return this.m_WareHouseId;
			}
			set
			{
				this.m_WareHouseId = value;
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
		/// 创建年份
		/// </summary>
		[DataMember]
		[DisplayName("创建年份")]
		public virtual Int32 CreatorYear
		{
			get
			{
				return this.m_CreatorYear;
			}
			set
			{
				this.m_CreatorYear = value;
			}
		}

		/// <summary>
		/// 创建月份
		/// </summary>
		[DataMember]
		[DisplayName("创建月份")]
		public virtual Int16 CreatorMonth
		{
			get
			{
				return this.m_CreatorMonth;
			}
			set
			{
				this.m_CreatorMonth = value;
			}
		}

		/// <summary>
		/// 领料人
		/// </summary>
		[DataMember]
		[DisplayName("领料人")]
		public virtual String PickingPeople
		{
			get
			{
				return this.m_PickingPeople;
			}
			set
			{
				this.m_PickingPeople = value;
			}
		}
		#endregion
	}
}