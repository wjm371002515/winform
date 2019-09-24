using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 商品信息(WareInfo)
	/// 对象号: 100079
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class WareInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 设备编号
		/// </summary>
		private String m_ItemNo = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 备件属类
		/// </summary>
		private Int32 m_ItemBigtype = 0;

		/// <summary>
		/// 备件类别
		/// </summary>
		private Int32 m_ItemType = 0;

		/// <summary>
		/// 数量
		/// </summary>
		private Int32 m_Amount = 0;

		/// <summary>
		/// 金额
		/// </summary>
		private Double m_Balance = 0.0;

		/// <summary>
		/// 低储预警
		/// </summary>
		private Int32 m_LowAmountWarning = 0;

		/// <summary>
		/// 超储预警
		/// </summary>
		private Int32 m_HighAmountWarning = 0;

		/// <summary>
		/// 库房编号
		/// </summary>
		private Int32 m_WareHouseId = 0;

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
		/// 设备编号
		/// </summary>
		[DataMember]
		[DisplayName("设备编号")]
		public virtual String ItemNo
		{
			get
			{
				return this.m_ItemNo;
			}
			set
			{
				this.m_ItemNo = value;
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
		/// 备件属类
		/// </summary>
		[DataMember]
		[DisplayName("备件属类")]
		public virtual Int32 ItemBigtype
		{
			get
			{
				return this.m_ItemBigtype;
			}
			set
			{
				this.m_ItemBigtype = value;
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
		/// 数量
		/// </summary>
		[DataMember]
		[DisplayName("数量")]
		public virtual Int32 Amount
		{
			get
			{
				return this.m_Amount;
			}
			set
			{
				this.m_Amount = value;
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
		/// 低储预警
		/// </summary>
		[DataMember]
		[DisplayName("低储预警")]
		public virtual Int32 LowAmountWarning
		{
			get
			{
				return this.m_LowAmountWarning;
			}
			set
			{
				this.m_LowAmountWarning = value;
			}
		}

		/// <summary>
		/// 超储预警
		/// </summary>
		[DataMember]
		[DisplayName("超储预警")]
		public virtual Int32 HighAmountWarning
		{
			get
			{
				return this.m_HighAmountWarning;
			}
			set
			{
				this.m_HighAmountWarning = value;
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