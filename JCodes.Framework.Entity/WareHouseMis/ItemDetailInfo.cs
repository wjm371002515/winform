using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 设备详细信息(ItemDetailInfo)
	/// 对象号: 100071
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class ItemDetailInfo : BaseEntity
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
		/// 供应商
		/// </summary>
		private String m_Manufacture = string.Empty;

		/// <summary>
		/// 图号
		/// </summary>
		private String m_MapNo = string.Empty;

		/// <summary>
		/// 规格型号
		/// </summary>
		private String m_Specification = string.Empty;

		/// <summary>
		/// 材质 
		/// </summary>
		private String m_Material = string.Empty;

		/// <summary>
		/// 备件属类
		/// </summary>
		private Int32 m_ItemBigtype = 0;

		/// <summary>
		/// 备件类别
		/// </summary>
		private Int32 m_ItemType = 0;

		/// <summary>
		/// 备件单位
		/// </summary>
		private Int32 m_ItemUnit = 0;

		/// <summary>
		/// 单价
		/// </summary>
		private Double m_Price = 0.0;

		/// <summary>
		/// 设备来源
		/// </summary>
		private String m_ItemSource = string.Empty;

		/// <summary>
		/// 库位
		/// </summary>
		private String m_StoragePos = string.Empty;

		/// <summary>
		/// 使用位置
		/// </summary>
		private String m_UsagePos = string.Empty;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;

		/// <summary>
		/// 库房编号
		/// </summary>
		private Int32 m_WareHouseId = 0;

		/// <summary>
		/// 部门Id
		/// </summary>
		private Int32 m_DeptId = 0;

		/// <summary>
		/// 数量
		/// </summary>
		private Int32 m_Amount = 0;

		/// <summary>
		/// 金额
		/// </summary>
		private Double m_Balance = 0.0;
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
		/// 图号
		/// </summary>
		[DataMember]
		[DisplayName("图号")]
		public virtual String MapNo
		{
			get
			{
				return this.m_MapNo;
			}
			set
			{
				this.m_MapNo = value;
			}
		}

		/// <summary>
		/// 规格型号
		/// </summary>
		[DataMember]
		[DisplayName("规格型号")]
		public virtual String Specification
		{
			get
			{
				return this.m_Specification;
			}
			set
			{
				this.m_Specification = value;
			}
		}

		/// <summary>
		/// 材质 
		/// </summary>
		[DataMember]
		[DisplayName("材质 ")]
		public virtual String Material
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				this.m_Material = value;
			}
		}

		/// <summary>
		/// 备件属类
		/// 1-国产标准件,
		/// 2-国产非标件,
		/// 3-进口标准件,
		/// 4-进口非标件
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
		/// 1-日常备件,
		/// 2-大修件,
		/// 3-技改件,
		/// 4-修复件,
		/// 5-事故件,
		/// 6-油品,
		/// 7-连一铜板,
		/// 8-连二铜板,
		/// 9-连铸维修区,
		/// 10-胶管承包,
		/// 11-电机承包,
		/// 12-其他承包件
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
		/// 备件单位
		/// 1-BA,
		/// 2-BAG,
		/// 3-BOT,
		/// 4-EA,
		/// 5-FU,
		/// 6-GE,
		/// 7-JIE,
		/// 8-KG,
		/// 9-KUA,
		/// 10-M1,
		/// 11-M2,
		/// 12-M3,
		/// 13-PA,
		/// 14-PC,
		/// 15-SHT,
		/// 16-SHU,
		/// 17-TAI,
		/// 18-TAO,
		/// 19-TIA,
		/// 20-TO,
		/// 21-ZHI,
		/// 22-ZI
		/// </summary>
		[DataMember]
		[DisplayName("备件单位")]
		public virtual Int32 ItemUnit
		{
			get
			{
				return this.m_ItemUnit;
			}
			set
			{
				this.m_ItemUnit = value;
			}
		}

		/// <summary>
		/// 单价
		/// </summary>
		[DataMember]
		[DisplayName("单价")]
		public virtual Double Price
		{
			get
			{
				return this.m_Price;
			}
			set
			{
				this.m_Price = value;
			}
		}

		/// <summary>
		/// 设备来源
		/// </summary>
		[DataMember]
		[DisplayName("设备来源")]
		public virtual String ItemSource
		{
			get
			{
				return this.m_ItemSource;
			}
			set
			{
				this.m_ItemSource = value;
			}
		}

		/// <summary>
		/// 库位
		/// </summary>
		[DataMember]
		[DisplayName("库位")]
		public virtual String StoragePos
		{
			get
			{
				return this.m_StoragePos;
			}
			set
			{
				this.m_StoragePos = value;
			}
		}

		/// <summary>
		/// 使用位置
		/// </summary>
		[DataMember]
		[DisplayName("使用位置")]
		public virtual String UsagePos
		{
			get
			{
				return this.m_UsagePos;
			}
			set
			{
				this.m_UsagePos = value;
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
		#endregion
	}
}