using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using JCodes.Framework.jCodesenum;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 通讯录联系人(AddressInfo)
	/// 对象号: 100009
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class AddressInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 性别
		/// </summary>
		private Int16 m_Gender = 0;

		/// <summary>
		/// 生日
		/// </summary>
		private DateTime m_Birthday = DateTime.Now;

		/// <summary>
		/// 手机
		/// </summary>
		private String m_MobilePhone = string.Empty;

		/// <summary>
		/// Email邮箱
		/// </summary>
		private String m_Email = string.Empty;

		/// <summary>
		/// QQ号
		/// </summary>
		private Int32 m_QQ = 0;

		/// <summary>
		/// 家庭电话
		/// </summary>
		private String m_HomePhone = string.Empty;

		/// <summary>
		/// 办公电话
		/// </summary>
		private String m_OfficePhone = string.Empty;

		/// <summary>
		/// 家庭地址
		/// </summary>
		private String m_HomeAddress = string.Empty;

		/// <summary>
		/// 办公地址
		/// </summary>
		private String m_OfficeAddress = string.Empty;

		/// <summary>
		/// 传真号码
		/// </summary>
		private String m_Fax = string.Empty;

		/// <summary>
		/// 公司名字
		/// </summary>
		private String m_CompanyName = string.Empty;

		/// <summary>
		/// 部门名字
		/// </summary>
		private String m_DeptName = string.Empty;

		/// <summary>
		/// 工作职位
		/// </summary>
		private String m_WorkPosition = string.Empty;

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
		/// 编辑人编号
		/// </summary>
		private Int32 m_EditorId = 0;

		/// <summary>
		/// 最后更新时间
		/// </summary>
		private DateTime m_LastUpdateTime = DateTime.Now;

		/// <summary>
		/// 部门Id
		/// </summary>
		private Int32 m_DeptId = 0;

		/// <summary>
		/// 公司Id
		/// </summary>
		private Int32 m_CompanyId = 0;

		/// <summary>
		/// 其他
		/// </summary>
		private String m_Other = string.Empty;

		/// <summary>
		/// 通讯录类型
		/// </summary>
		private AddressType m_AddressType;
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
		/// 性别
		/// </summary>
		[DataMember]
		[DisplayName("性别")]
		public virtual Int16 Gender
		{
			get
			{
				return this.m_Gender;
			}
			set
			{
				this.m_Gender = value;
			}
		}

		/// <summary>
		/// 生日
		/// </summary>
		[DataMember]
		[DisplayName("生日")]
		public virtual DateTime Birthday
		{
			get
			{
				return this.m_Birthday;
			}
			set
			{
				this.m_Birthday = value;
			}
		}

		/// <summary>
		/// 手机
		/// </summary>
		[DataMember]
		[DisplayName("手机")]
		public virtual String MobilePhone
		{
			get
			{
				return this.m_MobilePhone;
			}
			set
			{
				this.m_MobilePhone = value;
			}
		}

		/// <summary>
		/// Email邮箱
		/// </summary>
		[DataMember]
		[DisplayName("Email邮箱")]
		public virtual String Email
		{
			get
			{
				return this.m_Email;
			}
			set
			{
				this.m_Email = value;
			}
		}

		/// <summary>
		/// QQ号
		/// </summary>
		[DataMember]
		[DisplayName("QQ号")]
		public virtual Int32 QQ
		{
			get
			{
				return this.m_QQ;
			}
			set
			{
				this.m_QQ = value;
			}
		}

		/// <summary>
		/// 家庭电话
		/// </summary>
		[DataMember]
		[DisplayName("家庭电话")]
		public virtual String HomePhone
		{
			get
			{
				return this.m_HomePhone;
			}
			set
			{
				this.m_HomePhone = value;
			}
		}

		/// <summary>
		/// 办公电话
		/// </summary>
		[DataMember]
		[DisplayName("办公电话")]
		public virtual String OfficePhone
		{
			get
			{
				return this.m_OfficePhone;
			}
			set
			{
				this.m_OfficePhone = value;
			}
		}

		/// <summary>
		/// 家庭地址
		/// </summary>
		[DataMember]
		[DisplayName("家庭地址")]
		public virtual String HomeAddress
		{
			get
			{
				return this.m_HomeAddress;
			}
			set
			{
				this.m_HomeAddress = value;
			}
		}

		/// <summary>
		/// 办公地址
		/// </summary>
		[DataMember]
		[DisplayName("办公地址")]
		public virtual String OfficeAddress
		{
			get
			{
				return this.m_OfficeAddress;
			}
			set
			{
				this.m_OfficeAddress = value;
			}
		}

		/// <summary>
		/// 传真号码
		/// </summary>
		[DataMember]
		[DisplayName("传真号码")]
		public virtual String Fax
		{
			get
			{
				return this.m_Fax;
			}
			set
			{
				this.m_Fax = value;
			}
		}

		/// <summary>
		/// 公司名字
		/// </summary>
		[DataMember]
		[DisplayName("公司名字")]
		public virtual String CompanyName
		{
			get
			{
				return this.m_CompanyName;
			}
			set
			{
				this.m_CompanyName = value;
			}
		}

		/// <summary>
		/// 部门名字
		/// </summary>
		[DataMember]
		[DisplayName("部门名字")]
		public virtual String DeptName
		{
			get
			{
				return this.m_DeptName;
			}
			set
			{
				this.m_DeptName = value;
			}
		}

		/// <summary>
		/// 工作职位
		/// </summary>
		[DataMember]
		[DisplayName("工作职位")]
		public virtual String WorkPosition
		{
			get
			{
				return this.m_WorkPosition;
			}
			set
			{
				this.m_WorkPosition = value;
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
		/// 编辑人编号
		/// </summary>
		[DataMember]
		[DisplayName("编辑人编号")]
		public virtual Int32 EditorId
		{
			get
			{
				return this.m_EditorId;
			}
			set
			{
				this.m_EditorId = value;
			}
		}

		/// <summary>
		/// 最后更新时间
		/// </summary>
		[DataMember]
		[DisplayName("最后更新时间")]
		public virtual DateTime LastUpdateTime
		{
			get
			{
				return this.m_LastUpdateTime;
			}
			set
			{
				this.m_LastUpdateTime = value;
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
		/// 公司Id
		/// </summary>
		[DataMember]
		[DisplayName("公司Id")]
		public virtual Int32 CompanyId
		{
			get
			{
				return this.m_CompanyId;
			}
			set
			{
				this.m_CompanyId = value;
			}
		}

		/// <summary>
		/// 其他
		/// </summary>
		[DataMember]
		[DisplayName("其他")]
		public virtual String Other
		{
			get
			{
				return this.m_Other;
			}
			set
			{
				this.m_Other = value;
			}
		}

		/// <summary>
		/// 通讯录类型
		/// [个人,公司]
		/// </summary>
		[DataMember]
		public virtual AddressType AddressType
		{
			get
			{
				return this.m_AddressType;
			}
			set
			{
				this.m_AddressType = value;
			}
		}
		#endregion
	}
}