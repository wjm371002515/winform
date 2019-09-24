using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 部门机构信息(OUInfo)
	/// 对象号: 100061
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class OUInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 父节点ID序号
		/// </summary>
		private Int32 m_Pid = 0;

		/// <summary>
		/// 机构编码
		/// </summary>
		private String m_OuCode = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 排序
		/// </summary>
		private String m_Seq = string.Empty;

		/// <summary>
		/// 机构分类
		/// </summary>
		private Int16 m_OuType = 0;

		/// <summary>
		/// 地址
		/// </summary>
		private String m_Address = string.Empty;

		/// <summary>
		/// 外线电话
		/// </summary>
		private String m_OutPhone = string.Empty;

		/// <summary>
		/// 内线电话
		/// </summary>
		private String m_InnerPhone = string.Empty;

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
		/// 是否删除
		/// </summary>
		private Int16 m_IsDelete = 0;

		/// <summary>
		/// 是否禁用
		/// </summary>
		private Int16 m_IsForbid = 0;

		/// <summary>
		/// 公司Id
		/// </summary>
		private Int32 m_CompanyId = 0;
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
		/// 父节点ID序号
		/// </summary>
		[DataMember]
		[DisplayName("父节点ID序号")]
		public virtual Int32 Pid
		{
			get
			{
				return this.m_Pid;
			}
			set
			{
				this.m_Pid = value;
			}
		}

		/// <summary>
		/// 机构编码
		/// </summary>
		[DataMember]
		[DisplayName("机构编码")]
		public virtual String OuCode
		{
			get
			{
				return this.m_OuCode;
			}
			set
			{
				this.m_OuCode = value;
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
		/// 排序
		/// </summary>
		[DataMember]
		[DisplayName("排序")]
		public virtual String Seq
		{
			get
			{
				return this.m_Seq;
			}
			set
			{
				this.m_Seq = value;
			}
		}

		/// <summary>
		/// 机构分类
		/// </summary>
		[DataMember]
		[DisplayName("机构分类")]
		public virtual Int16 OuType
		{
			get
			{
				return this.m_OuType;
			}
			set
			{
				this.m_OuType = value;
			}
		}

		/// <summary>
		/// 地址
		/// </summary>
		[DataMember]
		[DisplayName("地址")]
		public virtual String Address
		{
			get
			{
				return this.m_Address;
			}
			set
			{
				this.m_Address = value;
			}
		}

		/// <summary>
		/// 外线电话
		/// </summary>
		[DataMember]
		[DisplayName("外线电话")]
		public virtual String OutPhone
		{
			get
			{
				return this.m_OutPhone;
			}
			set
			{
				this.m_OutPhone = value;
			}
		}

		/// <summary>
		/// 内线电话
		/// </summary>
		[DataMember]
		[DisplayName("内线电话")]
		public virtual String InnerPhone
		{
			get
			{
				return this.m_InnerPhone;
			}
			set
			{
				this.m_InnerPhone = value;
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
		/// 是否删除
		/// </summary>
		[DataMember]
		[DisplayName("是否删除")]
		public virtual Int16 IsDelete
		{
			get
			{
				return this.m_IsDelete;
			}
			set
			{
				this.m_IsDelete = value;
			}
		}

		/// <summary>
		/// 是否禁用
		/// </summary>
		[DataMember]
		[DisplayName("是否禁用")]
		public virtual Int16 IsForbid
		{
			get
			{
				return this.m_IsForbid;
			}
			set
			{
				this.m_IsForbid = value;
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
		#endregion
	}
}